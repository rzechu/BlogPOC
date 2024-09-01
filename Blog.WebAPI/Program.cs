using Blog.WebAPI.Configurations;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

try
{
    Log.Information($"starting {nameof(Blog.WebAPI)} server.");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });

    builder.Services.AddControllers();

	builder.Services.ConfigureServices(builder.Configuration);

	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

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