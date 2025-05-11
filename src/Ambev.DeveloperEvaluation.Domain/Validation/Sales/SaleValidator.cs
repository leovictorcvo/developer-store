using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sales;

/// <summary>
/// Validator for the Sale entity.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(s => s.Id).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(s => s.CreatedAt).NotEqual(default(DateTime));
        RuleFor(s => s.Customer).NotEmpty().SetValidator(new SaleCustomerValidator());
        RuleFor(s => s.Branch).NotEmpty();
        RuleFor(s => s.Items).NotEmpty().DependentRules(() =>
        {
            RuleFor(s => s.TotalAmount).Equal(s => s.Items.Where(i => !i.IsCancelled).Sum(i => i.TotalAmount));
            RuleForEach(s => s.Items).SetValidator(new SaleItemValidator());
        });
    }
}