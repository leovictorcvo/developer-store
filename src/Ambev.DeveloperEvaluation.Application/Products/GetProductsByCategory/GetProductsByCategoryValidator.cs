using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;

/// <summary>
/// Validator for the GetProductsByCategoryCommand.
/// </summary>
public class GetProductsByCategoryValidator : AbstractValidator<GetProductsByCategoryCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductsByCategoryValidator"/> class.
    /// </summary>
    public GetProductsByCategoryValidator()
    {
        RuleFor(x => x.CategoryName).NotEmpty().MaximumLength(100);
    }
}