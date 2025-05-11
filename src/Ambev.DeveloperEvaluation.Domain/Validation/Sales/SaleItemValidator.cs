using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sales;

/// <summary>
/// Validator for the SaleItem entity.
/// </summary>
public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(s => s.Id).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(s => s.Product).NotEmpty().SetValidator(new SaleItemProductValidator());
        RuleFor(s => s.Quantity).InclusiveBetween(1, 20);
    }
}