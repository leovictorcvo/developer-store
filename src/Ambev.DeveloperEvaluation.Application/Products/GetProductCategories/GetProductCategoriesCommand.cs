using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;

/// <summary>
/// Command to retrieve all product categories
/// </summary>
[ExcludeFromCodeCoverage]
public class GetProductCategoriesCommand : IRequest<GetProductCategoriesResult>
{
}