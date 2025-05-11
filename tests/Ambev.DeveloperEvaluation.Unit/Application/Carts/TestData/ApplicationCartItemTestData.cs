using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;

public static partial class ApplicationCartsTestData
{
    public static class ApplicationCartItemTestData
    {
        private static readonly Faker _faker = new();

        public static Guid GetValidCartItemProductId() => _faker.Random.Guid();

        public static int GetValidCartItemQuantity() => _faker.Random.Int(1, 20);

        public static CartItem GetValidCartItem() => new()
        {
            ProductId = GetValidCartItemProductId(),
            Quantity = GetValidCartItemQuantity()
        };
    }
}