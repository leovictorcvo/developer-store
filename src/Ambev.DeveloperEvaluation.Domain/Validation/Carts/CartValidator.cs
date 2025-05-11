using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Carts;

/// <summary>
/// Validator for the Cart class.
/// </summary>
public class CartValidator : AbstractValidator<Cart>
{
    /// <summary>
    /// Validator for the Cart class.
    /// </summary>
    public CartValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty)
            .WithMessage("UserId is required and cannot be empty.");

        RuleFor(x => x.Date).NotEmpty();

        RuleFor(x => x.Products).NotEmpty().WithMessage("Products are required.");
        RuleForEach(x => x.Products).SetValidator(new CartItemValidator());
    }
}