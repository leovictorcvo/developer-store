using Ambev.DeveloperEvaluation.Domain.Validation.Products;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;

/// <summary>
/// Validator for ProductRequest that defines validation rules for product creation and update.
/// </summary>
public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the ProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Required, must be between 3 and 100 characters
    /// - Price: Required, must be greater than zero
    /// - Description: Required, must be between 3 and 300 characters
    /// - Category: Required, must be between 3 and 100 characters
    /// - Image: Required, must be between 3 and 100 characters
    /// - Rating: Required and must be valid (using RatingValidator)
    /// </remarks>
    public ProductRequestValidator()
    {
        RuleFor(product => product.Title).NotEmpty().Length(3, 100);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Description).NotEmpty().Length(3, 300);
        RuleFor(product => product.Category).NotEmpty().Length(3, 100);
        RuleFor(product => product.Image).NotEmpty().Length(3, 100);
        RuleFor(product => product.Rating).NotNull().SetValidator(new RatingValidator());
    }
}