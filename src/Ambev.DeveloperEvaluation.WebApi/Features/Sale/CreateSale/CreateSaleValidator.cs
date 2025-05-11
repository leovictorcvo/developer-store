using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest.
/// </summary>
public class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleValidator class.
    /// </summary>
    public CreateSaleValidator()
    {
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