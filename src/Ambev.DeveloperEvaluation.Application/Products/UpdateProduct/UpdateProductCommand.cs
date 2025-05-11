using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Products.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Command to update an existing product.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateProductCommand : ApplicationProductCommand, IRequest<ApplicationProductResult>
{
    /// <summary>
    /// The product's unique identifier.
    /// </summary>
    public Guid Id { get; set; }
}