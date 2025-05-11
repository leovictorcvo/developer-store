using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;

/// <summary>
/// Represents a request to create or update a product in the system.
/// </summary>
public abstract class ProductRequest
{
    /// <summary>
    /// Gets the product's title. Must be unique and contain only valid characters.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Gets the product's full price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the product's description.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// Gets the product's category name.
    /// </summary>
    public string Category { get; set; } = default!;

    /// <summary>
    /// Gets the product's cover image.
    /// </summary>
    public string Image { get; set; } = default!;

    /// <summary>
    /// Gets the product's rating.
    /// </summary>
    public Rating Rating { get; set; } = default!;
}