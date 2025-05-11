namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.Common;

/// <summary>
/// Represents a product in a sale response.
/// </summary>
public class SaleResponseItemProduct
{
    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }
}