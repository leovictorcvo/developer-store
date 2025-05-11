using Ambev.DeveloperEvaluation.Domain.Validation.Sales;
using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

/// <summary>
/// Represents a product associated with a sale item.
/// </summary>
public record SaleItemProduct
{
    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    [property: BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    [property: BsonElement(nameof(Name))]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    [property: BsonElement(nameof(UnitPrice))]
    public decimal UnitPrice { get; init; }

    private SaleItemProduct() { }
    public SaleItemProduct(Guid id, string name, decimal unitPrice)
    {
        Id = id;
        Name = name;
        UnitPrice = unitPrice;

        Validate();
    }

    private void Validate()
    {
        var validator = new SaleItemProductValidator();
        var validationResult = validator.Validate(this);

        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);
    }
}