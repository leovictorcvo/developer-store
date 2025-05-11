using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using Bogus;
using VO = Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

public static class ApplicationSaleTestData
{
    private static readonly Faker _faker = new();

    public static Guid GetValidResourceId() => _faker.Random.Guid();

    public static string GetValidBranch() => _faker.Address.City();

    public static DateTime GetValidDateTime() => _faker.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddHours(2));

    public static User GetValidCustomer(Guid? userId = null) => new()
    {
        Id = userId ?? GetValidResourceId(),
        Name = new()
        {
            FirstName = ApplicationCartsTestData.GetValidCustomerFirstName(),
            LastName = ApplicationCartsTestData.GetValidCustomerLastName()
        }
    };

    public static VO.SaleCustomer GetValidSaleCustomer() => new(GetValidResourceId(), _faker.Name.FullName());

    public static List<VO.SaleItem> GenerateValidItems(int? quantity = null)
    {
        List<VO.SaleItem> items = [];
        quantity ??= _faker.Random.Int(1, 5);
        for (int i = 0; i < quantity; i++)
        {
            items.Add(ApplicationSaleItemTestData.GetValidSaleItem());
        }
        return items;
    }

    public static decimal GetValidTotalAmount() => _faker.Random.Decimal(0.01m, 1000.00m);

    public static CreateSaleCommand GetValidCreateSaleCommand() => new()
    {
        CustomerId = GetValidResourceId(),
        CartId = GetValidResourceId(),
        Branch = GetValidBranch(),
    };

    public static UpdateSaleCommand GetValidUpdateSaleCommand() => new()
    {
        SaleId = GetValidResourceId(),
        CustomerId = GetValidResourceId(),
        CartId = GetValidResourceId(),
        Branch = GetValidBranch(),
    };

    public static Sale GetValidSale()
    {
        var items = GenerateValidItems();
        var totalAmount = items.Where(i => !i.IsCancelled).Sum(i => i.TotalAmount);
        return new(
        GetValidResourceId(),
        GetValidDateTime(),
        GetValidSaleCustomer(),
        GetValidBranch(),
        totalAmount,
        [.. items]
        );
    }

    public static ApplicationSaleResult GetApplicationSaleResultFromSale(Sale sale) => new()
    {
        Id = sale.Id,
        CreatedAt = sale.CreatedAt,
        Customer = new() { Id = sale.Customer!.Id, Name = sale.Customer.Name },
        Branch = sale.Branch,
        Items = [.. sale.Items.Select(s =>
                new SaleItem()
                {
                    Id = s.Id,
                    Product = new() { Id = s.Product!.Id, Name = s.Product!.Name, UnitPrice = s.Product!.UnitPrice },
                    Quantity = s.Quantity,
                    IsCancelled = s.IsCancelled,
                    Discount = s.Discount,
                    TotalAmount = s.TotalAmount
                })],
    };
}