using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Products;

/// <summary>
/// Validator for Rating
/// </summary>
public class RatingValidator : AbstractValidator<Rating>
{
    /// <summary>
    /// Initializes a new instance of the RatingValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Rate: Required, must be between 0 and 5
    /// - Cout: Required, must be greater than or equal to 0
    /// </remarks>
    public RatingValidator()
    {
        RuleFor(rating => rating.Rate).InclusiveBetween(0, 5);
        RuleFor(rating => rating.Count).GreaterThanOrEqualTo(0);
    }
}