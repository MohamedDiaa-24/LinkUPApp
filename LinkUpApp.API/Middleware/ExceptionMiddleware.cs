using System;
using System.Net;
using System.Text.Json;
using LinkUpApp.API.Errors;

namespace LinkUpApp.API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
                IHostEnvironment host)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {

            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var Response = host.IsDevelopment() ?
            new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace) :
            new ApiException(context.Response.StatusCode, ex.Message, "Internal server error");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(Response, options);
            await context.Response.WriteAsync(json);
        }
    }
}
