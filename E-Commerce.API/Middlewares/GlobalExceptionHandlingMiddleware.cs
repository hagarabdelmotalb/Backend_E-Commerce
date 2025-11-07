using Domain.Exepctions;
using Shared.ErrorModels;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next; 
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try 
            { 
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound && !context.Response.HasStarted)
                    await HandleNotFoundApiAsync(context);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"somthing went wrong =>: {ex.Message } ");
                await HandleExceptionAsync(context,ex);
            }
        }
        private async Task HandleNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"The endpoint with url {context.Request.Path} not found"
            }.ToString();
            await context.Response.WriteAsync(response);
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                
                ErrorMessage = exception.Message
            };

            context.Response.StatusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedExecption => StatusCodes.Status401Unauthorized,

                ValidationExecption validationException => HandleValidationException(validationException, response),

                (_) => StatusCodes.Status500InternalServerError
            };

            
            response.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsJsonAsync(response.ToString());
        }

        private int HandleValidationException(ValidationExecption validationException, ErrorDetails response)
        {
            response.Errors = validationException.Errors;
            return StatusCodes.Status400BadRequest;

        }
    }
}
