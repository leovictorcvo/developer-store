using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth.TestData;

public static class AuthHandlerTestData
{
    private static readonly Faker _faker = new();

    public static User GetActiveUser()
    {
        var user = ApplicationUserHandlerTestData.GetValidUser();
        user.Activate();
        return user;
    }

    public static User GetInactiveUser()
    {
        var user = ApplicationUserHandlerTestData.GetValidUser();
        user.Deactivate();
        return user;
    }

    public static AuthenticateUserCommand GetValidAuthenticateUserCommandFromUser(User user) => new()
    {
        Email = user.Email,
        Password = _faker.Internet.Password(16)
    };

    public static AuthenticateUserResult GetValidAuthenticateUserResultFromUser(User user) => new()
    {
        Token = _faker.Internet.Password(32),
        Email = user.Email,
        Name = user.Username,
        Role = user.Role.ToString()
    };
}