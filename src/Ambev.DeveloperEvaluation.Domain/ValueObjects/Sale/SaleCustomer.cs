using Ambev.DeveloperEvaluation.Domain.Validation.Sales;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

/// <summary>
/// Represents a customer in the sales domain.
/// </summary>
public record SaleCustomer
{
    [property: BsonRepresentation(BsonType.String)]
    public Guid Id { get; init; }
    [property: BsonElement(nameof(Name))]
    public string Name { get; init; } = string.Empty;
    public SaleCustomer(Guid id, string name)
    {
        Id = id;
        Name = name;

        Validate();
    }

    private void Validate()
    {
        var validator = new SaleCustomerValidator();
        var validationResult = validator.Validate(this);

        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);
    }
}