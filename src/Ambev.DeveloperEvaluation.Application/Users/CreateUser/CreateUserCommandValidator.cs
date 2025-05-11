using Ambev.DeveloperEvaluation.Application.Users.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Validator for creating a new user.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        Include(new ApplicationUserValidator());
    }
}