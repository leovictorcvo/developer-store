using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common;

/// <summary>
/// Represents a request to create or update a cart.
/// </summary>
public class CartRequest
{
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
    public CartItem[] Products { get; set; } = [];
}