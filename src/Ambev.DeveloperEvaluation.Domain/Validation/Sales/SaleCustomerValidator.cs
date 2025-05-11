using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sales;

/// <summary>
/// Validator for the SaleCustomer entity.
/// </summary>
public class SaleCustomerValidator : AbstractValidator<SaleCustomer?>
{
    public SaleCustomerValidator()
    {
        RuleFor(s => s).NotNull().DependentRules(() =>
        {
            RuleFor(s => s!.Id).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(s => s!.Name).NotEmpty();
        });
    }
}