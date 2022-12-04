using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
           
            try
            {
                // Request logger..
                string message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Log(message);
                await _next(context);
                watch.Stop();
                // Response logger..
                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " +
                    context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms ";
                _loggerService.Log(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + "" +
                " Error message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + "ms";
            _loggerService.Log(message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // convert to json
            var result = JsonConvert.SerializeObject(new {error = ex.Message},Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtention
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }

}
