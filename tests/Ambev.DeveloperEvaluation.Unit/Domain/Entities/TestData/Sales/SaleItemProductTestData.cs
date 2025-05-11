using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Sales;

public static class SaleItemProductTestData
{
    private static readonly Faker _faker = new();

    public static Guid GenerateValidItemProductId() => _faker.Random.Guid();

    public static string GenerateValidItemProductName() => _faker.Commerce.ProductName();

    public static decimal GenerateValidItemProductUnitPrice() => _faker.Random.Decimal(1, 100);

    public static SaleItemProduct GenerateValidSaleItemProduct() => new(
        GenerateValidItemProductId(),
        GenerateValidItemProductName(),
        GenerateValidItemProductUnitPrice()
    );
}