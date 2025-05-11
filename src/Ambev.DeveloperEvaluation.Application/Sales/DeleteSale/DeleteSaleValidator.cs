using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

internal class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
{
    public DeleteSaleValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("SaleId must be a valid GUID.");
    }
}