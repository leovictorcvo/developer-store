using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Products.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Command to retrieve a product by its ID
/// </summary>
[ExcludeFromCodeCoverage]
public record GetProductCommand(Guid Id) : IRequest<ApplicationProductResult>
{
}