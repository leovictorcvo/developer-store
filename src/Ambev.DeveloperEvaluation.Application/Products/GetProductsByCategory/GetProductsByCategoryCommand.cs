using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Products.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;

/// <summary>
/// Command to get products by category.
/// </summary>
[ExcludeFromCodeCoverage]
public class GetProductsByCategoryCommand : PaginationCommand, IRequest<PaginationResult<ApplicationProductResult>>
{
    /// <summary>
    /// The name of the category to filter products by.
    /// </summary>
    public string CategoryName { get; private set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductsByCategoryCommand"/> class.
    /// </summary>
    /// <param name="categoryName"></param>
    public void SetProductsCategory(string categoryName)
    {
        CategoryName = categoryName;
    }
}