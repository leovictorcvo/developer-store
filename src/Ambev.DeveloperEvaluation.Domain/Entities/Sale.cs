using Ambev.DeveloperEvaluation.Domain.Validation.Sales;
using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; init; }

        [BsonElement(nameof(CreatedAt))]
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        [BsonElement(nameof(Customer))]
        public SaleCustomer? Customer { get; init; }

        [BsonElement(nameof(Branch))]
        public string Branch { get; init; } = string.Empty;

        [BsonElement(nameof(TotalAmount))]
        public decimal TotalAmount { get; init; } = 0.0m;

        [BsonElement(nameof(Items))]
        public IReadOnlyList<SaleItem> Items { get; init; } = [];

        private Sale()
        { }

        public Sale(Guid id, DateTime createdAt, SaleCustomer customer, string branch, decimal totalAmount, List<SaleItem> items)
        {
            Id = id;
            CreatedAt = createdAt;
            Customer = customer;
            Branch = branch;
            TotalAmount = totalAmount;
            Items = items.AsReadOnly();

            Validate();
        }

        private void Validate()
        {
            var validator = new SaleValidator();
            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);
        }
    }
}