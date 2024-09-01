using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;
using Users.Core.Interfaces;
using Users.Infrastructure.Repositories;
using Users.UserAPI.Persistence;

try
{
    Log.Information($"starting {nameof(Users.UserAPI)} server.");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });

    // Add services to the container.
    builder.Services.AddControllers();
	builder.Services.AddDbContext<UserContext>(options =>
	{
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	});

	builder.Services.AddScoped<IUserRepository, UserRepository>();
	builder.Services.AddScoped<UserService>();

	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	builder.Services.AddHealthChecks()
		.AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), name: "SQL Server");

	var app = builder.Build();
	app.UseSerilogRequestLogging();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

	app.UseAuthorization();


	app.MapControllers();

	app.MapHealthChecks("/api/health", new HealthCheckOptions()
	{
		Predicate = _ => true,
		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
	});
	app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}