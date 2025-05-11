using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Users;

public class PhoneValidator : AbstractValidator<string>
{
    public PhoneValidator()
    {
        RuleFor(phone => phone)
            .NotEmpty()
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must start with '+' followed by 11-15 digits.");
    }
}