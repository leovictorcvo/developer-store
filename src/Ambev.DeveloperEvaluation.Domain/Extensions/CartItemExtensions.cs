using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Extensions;

/// <summary>
/// Extension methods for CartItem objects.
/// </summary>
public static class CartItemExtensions
{
    /// <summary>
    /// Groups a list of CartItem objects by ProductId and sums their quantities.
    /// </summary>
    /// <param name="products"></param>
    /// <returns></returns>
    public static List<CartItem> GroupItemsByProductId(this List<CartItem> products)
        => [.. products.GroupBy(item => item.ProductId)
            .Select(group => new CartItem
            {
                ProductId = group.Key,
                Quantity = group.Sum(item => item.Quantity)
            })];
}