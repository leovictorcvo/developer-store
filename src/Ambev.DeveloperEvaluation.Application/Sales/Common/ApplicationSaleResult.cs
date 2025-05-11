using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

/// <summary>
/// Represents the result of a sale.
/// </summary>
[ExcludeFromCodeCoverage]
public class ApplicationSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier for the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the date of the sale.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the customer associated with the sale.
    /// </summary>
    public SaleCustomer Customer { get; set; } = new();

    /// <summary>
    /// Gets or set the branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the list of items in the sale.
    /// </summary>
    public SaleItem[] Items { get; set; } = [];
}