using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Carts;

public static class CartItemTestData
{
    private static readonly Faker<CartItem> CartItemFaker = new Faker<CartItem>()
        .RuleFor(ci => ci.ProductId, f => f.Random.Guid())
        .RuleFor(ci => ci.Quantity, f => f.Random.Int(1, 20));

    public static CartItem GenerateValid()
    {
        return CartItemFaker.Generate();
    }
}