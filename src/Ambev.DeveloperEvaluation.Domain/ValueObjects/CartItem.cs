namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents an item in the shopping cart.
/// </summary>
public class CartItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the cart.
    /// </summary>
    public int Quantity { get; set; }
}