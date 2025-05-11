using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Users;

/// <summary>
/// Validator for user name
/// </summary>
public class UserNameValidator : AbstractValidator<UserName>
{
    /// <summary>
    /// Initializes a new instance of the UserNameValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Firstname: Required, length between 1 and 50 characters
    /// - Lastname: Required, length between 1 and 50 characters
    /// </remarks>
    public UserNameValidator()
    {
        RuleFor(user => user.FirstName).NotEmpty().Length(1, 50);
        RuleFor(user => user.LastName).NotEmpty().Length(1, 50);
    }
}