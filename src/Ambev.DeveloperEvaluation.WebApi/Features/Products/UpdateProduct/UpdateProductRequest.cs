using Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Represents a request to update a existing product in the system.
/// </summary>
public class UpdateProductRequest : ProductRequest
{
    /// <summary>
    /// The product's unique identifier.
    /// </summary>
    public Guid Id { get; set; }
}