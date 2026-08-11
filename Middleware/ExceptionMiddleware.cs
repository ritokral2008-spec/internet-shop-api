using InternetShop.Exceptions;
using InternetShop.DTOs.Errors;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
        private async Task HandleException(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch(exception)
            {
                case ProductNotFoundException:
                case CategoryNotFoundException:
                case OrderNotFoundException:
                case UserNotFoundException:

                _logger.LogError(
                    exception,
                    "Не можем найти предмет в базе данных при запросе {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;

                case NotEnoughStockException:

                _logger.LogError(
                    exception,
                    "Недостаточно товара на складе при запросе {Method}, {Path}",
                    context.Request.Method,
                    context.Request.Path);

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                break;

                case ValidationException:

                _logger.LogError(
                    exception,
                    "Неверно введёные данные при запросе {Method} {Path}. Попробуйте ещё раз",
                    context.Request.Method,
                    context.Request.Path);

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                default:

                _logger.LogError(
                    exception,
                    "Необработанное исключение при запросе {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            var response = new ErrorResponseDto
            {
                Status = context.Response.StatusCode,
                Message = exception.Message,
                Path = context.Request.Path,
                TimeStamp = DateTime.UtcNow
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
