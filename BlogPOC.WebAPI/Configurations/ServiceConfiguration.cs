using BlogPOC.Application.Interfaces;
using BlogPOC.Application.Services;

namespace BlogPOC.WebAPI.Configurations;

public static class ServiceConfiguration
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlogContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultCdonnection"));
        });
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<BlogPostService>();

        services.AddHttpClient(Core.Constants.USERAPI, client =>
        {
            client.BaseAddress = new Uri("http://blogpoc.userapi");
        });
        services.AddScoped<IUserService, UserService>();
    }
}