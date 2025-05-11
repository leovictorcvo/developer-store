using Ambev.DeveloperEvaluation.WebApi.Features.Users.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Validator for UpdateUserRequest that defines validation rules for user updating.
/// </summary>
public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    /// <summary>
    /// Initializes validation rules for UpdateUserRequest.
    /// </summary>
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("User ID must be a valid GUID.");

        Include(new UserRequestValidator());
    }
}