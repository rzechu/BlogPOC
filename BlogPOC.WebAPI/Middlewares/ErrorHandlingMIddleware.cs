using BlogPOC.Core.Middlewares;

namespace BlogPOC.WebAPI.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) 
    : ErrorHandlingMiddlewareBase<ErrorHandlingMiddleware>(logger), IMiddleware
{ }