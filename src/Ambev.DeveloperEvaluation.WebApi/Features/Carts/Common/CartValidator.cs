using Ambev.DeveloperEvaluation.Domain.Validation.Carts;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common;

public class CartValidator : AbstractValidator<CartRequest>
{
    public CartValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty)
            .WithMessage("UserId is required and cannot be empty.");
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.Products).NotEmpty().WithMessage("Products are required.");
        RuleForEach(x => x.Products).SetValidator(new CartItemValidator());
    }
}