using System;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class ExceptionMiddlewares
    {
        public static void ConfigureExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;
                    if (exception is UnauthorizedAccessException)
                    {
                        var result = JsonSerializer.Serialize(new { error = exception.Message.ToString() });
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(result);
                    }
                });
            });
        }
    }
}