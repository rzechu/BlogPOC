using Serilog;
using Users.Core.Exceptions;

namespace Users.UserAPI.Middlewares;
public class ErrorHandlingMiddleware<T>(ILogger<T> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);

        }
        catch (NotFoundException exNotFound)
        {
            logger.LogWarning(exNotFound, exNotFound.Message);
            Log.Warning(exNotFound, exNotFound.Message);

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(exNotFound.Message);


        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred while processing the request.");
            Log.Error(ex, "An unhandled exception occurred while processing the request.");
            
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync("Something went wrong");
        }
    }
}