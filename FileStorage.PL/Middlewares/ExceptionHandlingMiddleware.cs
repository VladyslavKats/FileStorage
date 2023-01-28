using FileStorage.BLL.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileStorage.PL.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(EntityDoesNotExistException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.NotFound, ex.Message);
            }
            catch (FileStorageException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.InternalServerError,"Internal Server Error");
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext,
            string messageForLogger,
            HttpStatusCode statusCode,
            string messageForClient)
        {
            if (statusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(messageForLogger);
            }
            else
            {
                _logger.LogInformation(messageForLogger);
            }
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;
            var response = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                Message = messageForClient
            });
            await httpContext.Response.WriteAsync(response);
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
