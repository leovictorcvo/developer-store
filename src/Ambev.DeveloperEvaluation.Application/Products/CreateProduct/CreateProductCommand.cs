using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Products.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command to create a new product.
/// </summary>
[ExcludeFromCodeCoverage]
public class CreateProductCommand : ApplicationProductCommand, IRequest<ApplicationProductResult>
{
}