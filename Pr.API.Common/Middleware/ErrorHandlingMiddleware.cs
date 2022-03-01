using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pr.API.Common.Exceptions;

namespace Pr.API.Common.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionVerboseAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch (HttpRequestException ex)
            {
                await HandleExceptionVerboseAsync(context, ex, ex.StatusCode ?? HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                await HandleExceptionVerboseAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private static Task HandleExceptionVerboseAsync<T>(HttpContext context, T ex, HttpStatusCode code) where T : Exception
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;

            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = ex.Message }));
        }

        private static Task HandleExceptionAsync<T>(HttpContext context, T ex, HttpStatusCode code) where T : Exception
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var errorId = Guid.NewGuid();
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonConvert.SerializeObject($"Error reference id: {errorId}"));
        }
    }
}
