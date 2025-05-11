using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Auth.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth;

public class AuthHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly AuthenticateUserHandler _handler;

    public AuthHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _handler = new AuthenticateUserHandler(_userRepository, _passwordHasher, _jwtTokenGenerator);
    }

    [Fact(DisplayName = "Valid request should returns AuthenticateUserResult")]
    public async Task Handle_ValidRequest_ReturnsAuthenticateUserResult()
    {
        // Arrange
        User user = AuthHandlerTestData.GetActiveUser();
        AuthenticateUserCommand command = AuthHandlerTestData.GetValidAuthenticateUserCommandFromUser(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);
        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(Task.FromResult<User?>(user));
        var expectedResult = AuthHandlerTestData.GetValidAuthenticateUserResultFromUser(user);
        _jwtTokenGenerator.GenerateToken(user).Returns(expectedResult.Token);
        AuthenticateUserResult? result = null;

        // Act
        var method = async () => result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await method.Should().NotThrowAsync();
        result.Should().NotBeNull();
        result!.Token.Should().NotBeNullOrEmpty();
        result.Email.Should().Be(expectedResult.Email);
        result.Name.Should().Be(expectedResult.Name);
        result.Role.Should().Be(expectedResult.Role);
    }

    [Fact(DisplayName = "Request with invalid email should returns UnauthorizedAccessException")]
    public async Task Handle_InvalidEmail_ReturnsUnauthorizedAccessException()
    {
        // Arrange
        User user = AuthHandlerTestData.GetActiveUser();
        AuthenticateUserCommand command = AuthHandlerTestData.GetValidAuthenticateUserCommandFromUser(user);
        command.Email = "invalid@email.com";
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);
        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(Task.FromResult<User?>(null));

        // Act
        var method = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await method.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact(DisplayName = "Request with invalid password should returns UnauthorizedAccessException")]
    public async Task Handle_InvalidPassword_ReturnsUnauthorizedAccessException()
    {
        // Arrange
        User user = AuthHandlerTestData.GetActiveUser();
        AuthenticateUserCommand command = AuthHandlerTestData.GetValidAuthenticateUserCommandFromUser(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(false);
        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(Task.FromResult<User?>(user));

        // Act
        var method = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await method.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact(DisplayName = "Request with inactive user should returns UnauthorizedAccessException")]
    public async Task Handle_Inactive_ReturnsUnauthorizedAccessException()
    {
        // Arrange
        User user = AuthHandlerTestData.GetInactiveUser();
        AuthenticateUserCommand command = AuthHandlerTestData.GetValidAuthenticateUserCommandFromUser(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);
        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(Task.FromResult<User?>(user));

        // Act
        var method = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await method.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}