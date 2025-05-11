namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents the product rating.
/// </summary>
public class Rating
{
    /// <summary>
    /// The rating value.
    /// </summary>
    public decimal Rate { get; init; }

    /// <summary>
    /// The rating count.
    /// </summary>
    public int Count { get; init; }
}