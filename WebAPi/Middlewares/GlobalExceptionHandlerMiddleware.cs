
using Common.Responses;
using FluentValidation;
using System.Net;

namespace WebAPi.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            var statusCode = HttpStatusCode.InternalServerError;
            FailClientResponse response;
            switch (e)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = new FailClientResponse(message: e.Message,
                        responseErrors: validationException.Errors
                            .Select(x =>
                                new ResponseError(x.ErrorCode, x.PropertyName, x.ErrorMessage)).ToList(),

                        details: e.InnerException?.Message ?? "");
                    break;
                default:
                    response = new FailClientResponse(message: e.Message,
                        details: e.InnerException?.Message ?? "");
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}