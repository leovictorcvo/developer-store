namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.Common;

/// <summary>
/// Represents a customer associated with a sale.
/// </summary>
public class SaleResponseCustomer
{
    /// <summary>
    /// Gets or sets the unique identifier for the customer.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}