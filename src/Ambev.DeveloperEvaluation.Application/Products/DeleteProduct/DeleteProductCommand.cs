using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Command for deleting a product
/// </summary>
[ExcludeFromCodeCoverage]
public record DeleteProductCommand(Guid Id) : IRequest<Unit>
{
}