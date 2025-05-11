using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

/// <summary>
/// Represents an item in a sale.
/// </summary>
[ExcludeFromCodeCoverage]
public class SaleItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the sale item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the product associated with the sale item.
    /// </summary>
    public SaleItemProduct Product { get; set; } = new();

    /// <summary>
    /// Gets or sets if the item was cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to the sale item.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the total amount for the sale item after applying the discount.
    /// </summary>
    public decimal TotalAmount { get; set; }
}