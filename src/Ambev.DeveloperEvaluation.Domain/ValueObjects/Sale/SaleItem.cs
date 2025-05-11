using Ambev.DeveloperEvaluation.Domain.Extensions;
using Ambev.DeveloperEvaluation.Domain.Validation.Sales;
using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

public record SaleItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the sale item.
    /// </summary>
    [property: BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the product associated with the sale item.
    /// </summary>
    [property: BsonElement(nameof(Product))]
    public SaleItemProduct? Product { get; init; }

    /// <summary>
    /// Gets or sets indicates whether the sale item is cancelled.
    /// </summary>
    [BsonElement(nameof(IsCancelled))]
    public bool IsCancelled { get; init; }

    /// <summary>
    /// Gets or sets the quantity of the product sold.
    /// </summary>
    [property: BsonElement(nameof(Quantity))]
    public int Quantity { get; init; }

    /// <summary>
    /// Gets or sets the discount applied to the sale item.
    /// </summary>
    [property: BsonElement(nameof(Discount))]
    public decimal Discount { get; init; }

    /// <summary>
    /// Gets or sets the total amount for the sale item after applying the discount.
    /// </summary>
    [property: BsonElement(nameof(TotalAmount))]
    public decimal TotalAmount { get; init; }

    private SaleItem() { }

    public SaleItem(Guid id, SaleItemProduct product, int quantity, bool isCancelled)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
        IsCancelled = isCancelled;

        Validate();
        decimal total = Math.Round(quantity * product.UnitPrice, 2);
        decimal discount = this.CalculateDiscount(total);

        Discount = discount;
        TotalAmount = total - discount;
    }

    private void Validate()
    {
        var validator = new SaleItemValidator();
        var validationResult = validator.Validate(this);

        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);
    }
}