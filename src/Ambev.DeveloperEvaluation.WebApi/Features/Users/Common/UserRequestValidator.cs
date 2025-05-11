using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Common;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    /// <summary>
    /// Initializes a new instance of the UserRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Address: Required and must be valid (using UserAddressValidator)
    /// - Name: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+XXXXXXXXXXX)
    /// - Status: Must be valid and cannot be Unknown
    /// - Role: Must be valid and cannot be None
    /// </remarks>
    public UserRequestValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Username).NotEmpty().Length(3, 50);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Address).NotNull().WithMessage("Address is required.").SetValidator(new UserAddressValidator());
        RuleFor(user => user.Name).NotNull().WithMessage("Name is required.").SetValidator(new UserNameValidator());
        RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
        RuleFor(user => user.Status).Must(BeValidStatus).WithMessage("Status must be Active, Inactive or Suspended.");
        RuleFor(user => user.Role).Must(BeValidRole).WithMessage("Role must be Customer, Manager or Admin.");
    }

    private static bool BeValidStatus(string status)
    {
        return Enum.TryParse<UserStatus>(status, true, out var userStatus) && userStatus != UserStatus.Unknown;
    }

    private static bool BeValidRole(string role)
    {
        return Enum.TryParse<UserRole>(role, true, out var userRole) && userRole != UserRole.None;
    }
}