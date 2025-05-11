using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Sales;

public static class SaleTestData
{
    private static readonly Faker _faker = new();

    public static Guid GenerateValidId() => _faker.Random.Guid();

    public static DateTime GenerateValidCreatedAt() => _faker.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddHours(2));

    public static SaleCustomer GenerateValidCustomer() => SaleCustomerTestData.GenerateValidSaleCustomer();

    public static string GenerateValidBranch() => _faker.Address.City();

    public static (decimal totalAmount, List<SaleItem> items) GenerateValidItems(int? quantity = null)
    {
        List<SaleItem> items = [];
        quantity ??= _faker.Random.Int(1, 5);
        for (int i = 0; i < quantity; i++)
        {
            items.Add(SaleItemTestData.GenerateValidSaleItem());
        }
        decimal totalAmount = items.Where(i => !i.IsCancelled).Sum(i => i.TotalAmount);
        return (totalAmount, items);
    }

    public static Sale GenerateValidSale()
    {
        Guid saleId = GenerateValidId();
        DateTime createdAt = GenerateValidCreatedAt();
        var customer = GenerateValidCustomer();
        string branch = GenerateValidBranch();
        var (totalAmount, items) = GenerateValidItems(_faker.Random.Int(1, 5));
        return new Sale(saleId, createdAt, customer, branch, totalAmount, items);
    }
}