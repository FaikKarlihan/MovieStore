using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebApi.Application.DbLoggsOperations;
using WebApi.Services;

namespace WebApi.Middlwares
{
    /*
    While the captured response messages are written to the console, 
    they are also written to the Database with the DbLoggerCommand.
    */
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IloggerService _loggerService;

        /*
        "CustomExceptionMiddleware" is declared as a singleton.
        The DbLoggerCommand is scoped.
        A scoped dependency cannot be directly injected into a singleton object.
        So it is resolved at runtime using IServiceProvider.
        */
        private readonly IServiceProvider _serviceProvider;

        public CustomExceptionMiddleware(RequestDelegate next, IloggerService loggerService, IServiceProvider serviceProvider)
        {
            _next = next;
            _loggerService = loggerService;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbLogger = scope.ServiceProvider.GetRequiredService<DbLoggerCommand>();
                
                try
                {
                    string message = $"[Request] HTTP {context.Request.Method} - {context.Request.Path}";
                    _loggerService.Write(message);
                    dbLogger.Handle(message);

                    await _next(context);

                    watch.Stop();
                    message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} Responded {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds}ms";
                    _loggerService.Write(message);
                    dbLogger.Handle(message);
                }
                catch (Exception ex)
                {
                    watch.Stop();
                    await HandleException(context, ex, watch, dbLogger);
                }
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch, DbLoggerCommand dbLogger)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = $"[Error] HTTP {context.Request.Method} - {context.Response.StatusCode} Error Message {ex.Message} in {watch.Elapsed.TotalMilliseconds}ms";
            _loggerService.Write(message);
            dbLogger.Handle(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}