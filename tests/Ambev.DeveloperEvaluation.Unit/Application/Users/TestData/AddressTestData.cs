using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;

public static class AddressTestData
{
    private static readonly Faker<AddressGeolocation> addressGeolocationFaker = new Faker<AddressGeolocation>()
        .RuleFor(a => a.Lat, f => f.Address.Latitude().ToString())
        .RuleFor(a => a.Long, f => f.Address.Longitude().ToString());

    public static Faker<UserAddress> UserAddressFaker = new Faker<UserAddress>("pt_BR")
        .RuleFor(u => u.City, f => f.Address.City())
        .RuleFor(u => u.Street, f => f.Address.StreetName())
        .RuleFor(u => u.Number, f => f.Random.Int(1, 1000))
        .RuleFor(u => u.ZipCode, f => f.Address.ZipCode())
        .RuleFor(u => u.Geolocation, addressGeolocationFaker.Generate());
}