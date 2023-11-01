using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;

namespace Service.Monitor.Middleware;

public class ExceptionHandlerMiddleware
{
    public static async Task HandleExceptionsAsync(HttpContext context)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandlerMiddleware>>();

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = MediaTypeNames.Text.Plain;
        
        
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature == null)
            await context.Response.WriteAsync("An exception was thrown.");

        if (exceptionHandlerPathFeature?.Error is ArgumentException)
        {
            logger.LogInformation($"Bad request: {exceptionHandlerPathFeature.Error.Message}");
            logger.LogInformation($"Bad request on: {exceptionHandlerPathFeature.Path}");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
        }
        else if (exceptionHandlerPathFeature is { Error: not null })
        {
            if (exceptionHandlerPathFeature.Error.Message.Contains("no elements"))
            {
                logger.LogInformation($"Nothing found error: {exceptionHandlerPathFeature.Error.Message}");
                logger.LogInformation($"Nothing found on: {exceptionHandlerPathFeature.Path}");
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
            }
        }
    }
}