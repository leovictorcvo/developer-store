using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Carts.Common;

/// <summary>
/// Represents a response containing cart details.
/// </summary>
[ExcludeFromCodeCoverage]
public class ApplicationCartResult
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