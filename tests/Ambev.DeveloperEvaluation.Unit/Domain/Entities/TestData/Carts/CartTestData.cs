using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Carts;

public static class CartTestData
{
    private static readonly Faker<CartItem> CartItemFaker = new Faker<CartItem>()
        .RuleFor(ci => ci.ProductId, f => f.Random.Guid())
        .RuleFor(ci => ci.Quantity, f => f.Random.Int(1, 20));

    private static readonly Faker<Cart> CartFaker = new Faker<Cart>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.Date, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddHours(2)))
        .RuleFor(c => c.UserId, f => f.Random.Guid())
        .RuleFor(c => c.CreatedAt, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddHours(2)))
        .RuleFor(c => c.UpdatedAt, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddHours(2)))
        .RuleFor(c => c.Products, f => CartItemFaker.Generate(f.Random.Number(1, 5)));

    public static Cart GenerateValid()
    {
        return CartFaker.Generate();
    }

    public static List<CartItem> GenerateValidListCartItems()
    {
        var faker = new Faker();
        return CartItemFaker.Generate(faker.Random.Number(1, 10));
    }
}