using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Sales;

public static class SaleCustomerTestData
{
    public static SaleCustomer GenerateValidSaleCustomer()
    {
        var faker = new Bogus.Faker();
        return new SaleCustomer(faker.Random.Guid(), faker.Person.FullName);
    }
}
