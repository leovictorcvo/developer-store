using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductCategories
{
    /// <summary>
    /// Result class for GetProductCategories operation
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetProductCategoriesResult
    {
        /// <summary>
        /// List of product categories
        /// </summary>
        public List<string> CategoryNames { get; set; } = [];
    }
}