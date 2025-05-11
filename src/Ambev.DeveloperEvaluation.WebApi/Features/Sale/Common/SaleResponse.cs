namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.Common;

/// <summary>
/// Represents the response for a sale.
/// </summary>
public class SaleResponse
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
    public SaleResponseCustomer Customer { get; set; } = new();

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
    public SaleResponseItem[] Items { get; set; } = [];
}