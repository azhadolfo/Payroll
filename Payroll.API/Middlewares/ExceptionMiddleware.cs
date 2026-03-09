using FluentValidation;
using System.Net;

namespace Payroll.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            // FluentValidation error
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error occurred");

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Validation failed",
                    errors = ex.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            // Not found error
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found");

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message
                });
            }

            // Generic error
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "An unexpected error occurred"
                });
            }
        }
    }
}