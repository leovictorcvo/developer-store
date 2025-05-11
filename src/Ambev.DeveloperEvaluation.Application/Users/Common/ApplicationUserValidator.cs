using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.Common;

/// <summary>
/// Validator for ApplicationUserCommand that defines validation rules for user creation/updating command.
/// </summary>
public class ApplicationUserValidator : AbstractValidator<ApplicationUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Username: Required, must be between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Address: Required and must be valid (using UserAddressValidator)
    /// - Name: Required, length between 3 and 50 characters
    /// - Phone: Must match international format (+XXXXXXXXXXX)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// </remarks>
    public ApplicationUserValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Username).NotEmpty().Length(3, 50);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Address).NotNull().WithMessage("Address is required.").SetValidator(new UserAddressValidator());
        RuleFor(user => user.Name).NotNull().WithMessage("Name is required.").SetValidator(new UserNameValidator());
        RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
        RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
        RuleFor(user => user.Role).NotEqual(UserRole.None);
    }
}