using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common;

/// <summary>
/// Represents a response containing cart details.
/// </summary>
public class CartResponse
{
    /// <summary>
    /// The unique identifier of the cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The data that the cart was created or updated.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The list of products in the cart.
    /// </summary>
    public List<CartItem> Products { get; set; } = [];
}