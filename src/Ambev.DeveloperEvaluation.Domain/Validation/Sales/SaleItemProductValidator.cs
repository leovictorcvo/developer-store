using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sales;

/// <summary>
/// Validator for the SaleItemProduct entity.
/// </summary>
public class SaleItemProductValidator : AbstractValidator<SaleItemProduct?>
{
    public SaleItemProductValidator()
    {
        RuleFor(s => s).NotNull().DependentRules(() =>
        {
            //TODO: Check if productId is valid
            RuleFor(s => s!.Id).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(s => s!.Name).NotEmpty();
            RuleFor(s => s!.UnitPrice).GreaterThan(0);
        });
    }
}