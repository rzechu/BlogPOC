namespace BlogPOC.WebAPI.Configurations;

public static class ServiceConfiguration
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlogContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<BlogPostService>();

        services.AddHttpClient(Core.Constants.USERAPI, client =>
        {
            client.BaseAddress = new Uri("http://blogpoc.userapi");
        });
        services.AddScoped<IUserService, UserService>();

        services.AddHealthChecks()
        .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "SQL Server")
        .AddUrlGroup(new Uri("http://blogpoc.userapi/api/health"), name: "User API");
    }
}