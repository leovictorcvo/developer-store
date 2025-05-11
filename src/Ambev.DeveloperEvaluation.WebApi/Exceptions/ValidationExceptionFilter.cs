using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Exceptions
{
    public static class ValidationExceptionFilter
    {
        public static IActionResult HandleModelStateValidationError(ActionContext context)
        {
            var errors = context.ModelState
            .Where(e => (e.Value?.Errors.Count ?? 0) > 0)
            .Select(e => new ValidationErrorDetail
            {
                Error = "Validation error",
                Detail = e.Value!.Errors[0].ErrorMessage
            })
            .ToList();
            return new BadRequestObjectResult(new ApiResponse
            {
                Success = false,
                Message = "Validation failed",
                Errors = errors
            });
        }
    }
}