using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Sales;

public static class SaleItemTestData
{
    private static readonly Faker _faker = new();

    public static Guid GenerateValidSaleItemId() => _faker.Random.Guid();

    public static SaleItemProduct GenerateValidSaleItemProduct() => SaleItemProductTestData.GenerateValidSaleItemProduct();

    public static int GenerateValidSaleItemQuantity() => _faker.Random.Int(1, 20);

    public static bool GenerateValidSaleItemIsCancelled() => _faker.Random.Bool();

    public static SaleItem GenerateValidSaleItem() => new(
        GenerateValidSaleItemId(),
        GenerateValidSaleItemProduct(),
        GenerateValidSaleItemQuantity(),
        GenerateValidSaleItemIsCancelled()
        );
}