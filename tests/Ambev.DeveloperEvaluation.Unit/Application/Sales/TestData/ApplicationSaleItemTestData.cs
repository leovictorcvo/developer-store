using Bogus;

using VO = Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

/// <summary>
/// Static class containing test data for SaleItem.
/// </summary>
public static class ApplicationSaleItemTestData
{
    private static readonly Faker _faker = new();

    public static Guid GetValidResourceId() => _faker.Random.Guid();

    public static VO.SaleItemProduct GetValidSaleItemProduct() => new(
        id: GetValidResourceId(),
        name: _faker.Commerce.ProductName(),
        unitPrice: _faker.Random.Decimal(0.01m, 1000.00m)
        );

    public static VO.SaleItem GetValidSaleItem() => new(GetValidResourceId(), GetValidSaleItemProduct(), _faker.Random.Int(1, 20), false);
}