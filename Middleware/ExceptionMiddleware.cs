using InternetShop.Exceptions;
using InternetShop.Models;
using System.Text.Json;

namespace InternetShop.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private static async Task HandleException(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch(exception)
            {
                case ProductNotFoundException:
                case CategoryNotFoundException:
                case OrderNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            var response = new ErrorResponse
            {
                Status = context.Response.StatusCode,
                Message = exception.Message,
                TimeStamp = DateTime.Now
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
