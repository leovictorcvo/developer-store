namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductCategories;

/// <summary>
/// API response model for GetProductCategories operation
/// </summary>
public class GetProductCategoriesResponse
{
    /// <summary>
    /// The category names
    /// </summary>
    public List<string> CategoryNames { get; set; } = [];
}