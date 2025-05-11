using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ApplicationUserHandlerTestData
{
    private static readonly Faker<UserName> userNameFaker = new Faker<UserName>()
        .RuleFor(u => u.FirstName, f => f.Name.FirstName())
        .RuleFor(u => u.LastName, f => f.Name.LastName());

    /// <summary>
    /// Configures the Faker to generate valid createUserCommand.
    /// The generated users will have valid:
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static readonly Faker<CreateUserCommand> createUserHandlerFaker = new Faker<CreateUserCommand>()
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
        .RuleFor(u => u.Address, AddressTestData.UserAddressFaker.Generate())
        .RuleFor(u => u.Name, userNameFaker.Generate());

    /// <summary>
    /// Configures the Faker to generate valid updateUserCommand.
    /// The generated users will have valid:
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static readonly Faker<UpdateUserCommand> updateUserHandlerFaker = new Faker<UpdateUserCommand>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
        .RuleFor(u => u.Address, AddressTestData.UserAddressFaker.Generate())
        .RuleFor(u => u.Name, userNameFaker.Generate());

    /// <summary>
    /// Generates a valid CreateUserCommand with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CreateUserCommand with randomly generated data.</returns>
    public static CreateUserCommand GenerateValidCreateUserCommand() => createUserHandlerFaker.Generate();

    /// <summary>
    /// Generates a valid UpdateUserCommand with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid UpdateUserCommand with randomly generated data.</returns>
    public static UpdateUserCommand GenerateValidUpdateUserCommand() => updateUserHandlerFaker.Generate();

    private static readonly Faker<User> userFaker = new Faker<User>()
    .RuleFor(u => u.Id, f => Guid.NewGuid())
    .RuleFor(u => u.Username, f => f.Internet.UserName())
    .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
    .RuleFor(u => u.Email, f => f.Internet.Email())
    .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
    .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
    .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
    .RuleFor(u => u.Address, AddressTestData.UserAddressFaker.Generate())
    .RuleFor(u => u.Name, userNameFaker.Generate());

    public static User GetValidUser() => userFaker.Generate();

    public static Guid GetValidUserId() => new Faker().Random.Guid();

    public static UserRole GetValidUserRole() => new Faker().PickRandom<UserRole>();

    public static ApplicationUserResult GetApplicationUserResultFromUser(User user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        Phone = user.Phone,
        Status = user.Status.ToString(),
        Role = user.Role.ToString(),
        Address = user.Address,
        Name = user.Name
    };
}