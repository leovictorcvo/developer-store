using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Carts;

/// <summary>
/// Validator for the CartItem class.
/// </summary>
public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Product ID must not be empty and cannot be a default Guid value.");

        RuleFor(x => x.Quantity).InclusiveBetween(1, 20)
            .WithMessage("Quantity must be between 1 and 20.");
    }
}