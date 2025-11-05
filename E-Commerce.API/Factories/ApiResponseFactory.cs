using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.API.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState
                .Where(e => e.Value.Errors.Count > 0).Select(e => new ValidationError()
                {
                    Field = e.Key,
                    Errors = e.Value?.Errors.Select(er => er.ErrorMessage) ?? new List<string>()
                });
            var Response = new ValidationErrorResponse()
            {
                StatusCode = 400,
                ErrorMessage = "One or more validation errors occurred.",
                Errors = errors
            };
            return new BadRequestObjectResult(Response);
        }
    }
}
