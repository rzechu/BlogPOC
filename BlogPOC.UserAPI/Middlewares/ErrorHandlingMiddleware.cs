using BlogPOC.Core.Middlewares;

namespace BlogPOC.UserAPI.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) 
    : ErrorHandlingMiddlewareBase<ErrorHandlingMiddleware>(logger), IMiddleware
{ }