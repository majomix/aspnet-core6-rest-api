using DataProcessing.Application.Exceptions;
using System.Net;
using Newtonsoft.Json;

namespace DataProcessing.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var httpStatusCode = exception switch
            {
                NotFoundException _ => HttpStatusCode.NotFound,
                _ => HttpStatusCode.BadRequest
            };

            context.Response.StatusCode = (int)httpStatusCode;

            var result = JsonConvert.SerializeObject(new { error = exception.Message });

            return context.Response.WriteAsync(result);
        }
    }
}
