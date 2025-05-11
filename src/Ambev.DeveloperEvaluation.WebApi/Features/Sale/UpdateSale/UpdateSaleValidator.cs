using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.UpdateSale;

public class UpdateSaleValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("SaleId is required.");
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("CustomerId is required.");
        RuleFor(x => x.Branch)
            .NotEmpty()
            .WithMessage("Branch is required.");
        RuleFor(x => x.CartId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("CartId is required.");
    }
}