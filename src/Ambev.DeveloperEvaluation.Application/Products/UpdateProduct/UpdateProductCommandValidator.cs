using Ambev.DeveloperEvaluation.Application.Products.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithMessage("Product ID must be a valid GUID.");
            Include(new ApplicationProductCommandValidator());
        }
    }
}