using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;

public static partial class ApplicationCartsTestData
{
    private static readonly Faker _faker = new();

    public static Guid GetValidResourceIdentifier() => _faker.Random.Guid();

    public static DateTime GetValidCartDate() => _faker.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddHours(2));

    public static string GetValidCustomerFirstName() => _faker.Person.FirstName;

    public static string GetValidCustomerLastName() => _faker.Person.LastName;

    public static UserRole GetValidUserRole() => _faker.PickRandom<UserRole>();

    public static List<CartItem> GenerateValidItems(int? quantity = null)
    {
        List<CartItem> items = [];
        quantity ??= _faker.Random.Int(1, 5);
        for (int i = 0; i < quantity; i++)
        {
            items.Add(ApplicationCartItemTestData.GetValidCartItem());
        }
        return items;
    }

    public static CreateCartCommand GetValidCreateCartCommand()
    {
        var validUserId = GetValidResourceIdentifier();
        return new()
        {
            UserId = validUserId,
            RequestedBy = validUserId,
            ApplicantRole = GetValidUserRole(),
            Date = GetValidCartDate(),
            Products = GenerateValidItems()
        };
    }

    public static CreateCartCommand GetInvalidCreateCartCommand() => new()
    {
        UserId = Guid.Empty,
        RequestedBy = GetValidResourceIdentifier(),
        ApplicantRole = GetValidUserRole(),
        Date = GetValidCartDate(),
        Products = GenerateValidItems()
    };

    public static UpdateCartCommand GetValidUpdateCartCommand()
    {
        var validUserId = GetValidResourceIdentifier();
        return new()
        {
            UserId = validUserId,
            RequestedBy = validUserId,
            ApplicantRole = GetValidUserRole(),
            Date = GetValidCartDate(),
            Products = GenerateValidItems()
        };
    }

    public static UpdateCartCommand GetInvalidUpdateCartCommand() => new()
    {
        UserId = Guid.Empty,
        RequestedBy = GetValidResourceIdentifier(),
        ApplicantRole = GetValidUserRole(),
        Date = GetValidCartDate(),
        Products = GenerateValidItems()
    };

    public static Cart GetValidCart()
    {
        Cart cart = new();
        cart.Create(GetValidResourceIdentifier(), GetValidCartDate(), GenerateValidItems());
        return cart;
    }

    public static ApplicationCartResult GetApplicationCartResultFromCart(Cart cart) => new()
    {
        Id = cart.Id,
        UserId = cart.UserId,
        Date = cart.Date,
        Products = [.. cart.Products]
    };
}