using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Users;

public static class UserAddressTestData
{
    private static readonly Faker<AddressGeolocation> addressGeolocationFaker = new Faker<AddressGeolocation>()
        .RuleFor(a => a.Lat, f => f.Address.Latitude().ToString())
        .RuleFor(a => a.Long, f => f.Address.Longitude().ToString());

    private static readonly Faker<UserAddress> userAddressFaker = new Faker<UserAddress>("pt_BR")
        .RuleFor(u => u.City, f => f.Address.City())
        .RuleFor(u => u.Street, f => f.Address.StreetName())
        .RuleFor(u => u.Number, f => f.Random.Int(1, 1000))
        .RuleFor(u => u.ZipCode, f => f.Address.ZipCode())
        .RuleFor(u => u.Geolocation, addressGeolocationFaker.Generate());

    public static UserAddress GenerateValid()
    {
        return userAddressFaker.Generate();
    }

    public static UserAddress GenerateEmptyAddressStreet()
    {
        var address = userAddressFaker.Generate();
        address.Street = string.Empty;
        return address;
    }

    public static UserAddress GenerateBigAddressStreet()
    {
        var address = userAddressFaker.Generate();
        address.Street = new Faker().Random.String(101, 120);
        return address;
    }

    public static UserAddress GenerateEmptyAddressCity()
    {
        var address = userAddressFaker.Generate();
        address.City = string.Empty;
        return address;
    }

    public static UserAddress GenerateBigAddressCity()
    {
        var address = userAddressFaker.Generate();
        address.City = new Faker().Random.String(51, 120);
        return address;
    }

    public static UserAddress GenerateEmptyAddressNumber()
    {
        var address = userAddressFaker.Generate();
        address.Number = 0;
        return address;
    }

    public static UserAddress GenerateEmptyAddressZipCode()
    {
        var address = userAddressFaker.Generate();
        address.ZipCode = string.Empty;
        return address;
    }

    public static UserAddress GenerateInvalidAddressZipCode()
    {
        var address = userAddressFaker.Generate("en_US");
        address.ZipCode = new Faker().Address.ZipCode();
        return address;
    }
}