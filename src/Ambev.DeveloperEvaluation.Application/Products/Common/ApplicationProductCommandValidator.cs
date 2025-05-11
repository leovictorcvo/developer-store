using Ambev.DeveloperEvaluation.Domain.Validation.Products;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Common;

public class ApplicationProductCommandValidator : AbstractValidator<ApplicationProductCommand>
{
    public ApplicationProductCommandValidator()
    {
        RuleFor(product => product.Title).NotEmpty().Length(3, 100);
        RuleFor(product => product.Description).NotEmpty().Length(3, 300);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Category).NotEmpty().Length(3, 100);

        RuleFor(product => product.Image)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Product image must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Product image cannot be longer than 100 characters.");

        RuleFor(product => product.Rating)
            .NotNull()
            .SetValidator(new RatingValidator());
    }
}