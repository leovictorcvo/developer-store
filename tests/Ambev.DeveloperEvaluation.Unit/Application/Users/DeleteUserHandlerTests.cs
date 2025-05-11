using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

public class DeleteUserHandlerTests
{
    private readonly IUserRepository _UserRepository;
    private readonly DeleteUserHandler _handler;

    public DeleteUserHandlerTests()
    {
        _UserRepository = Substitute.For<IUserRepository>();
        _handler = new DeleteUserHandler(_UserRepository);
    }

    [Fact(DisplayName = "Given valid command When handle Then delete User")]
    public async Task Handle_ValidRequest_ShouldDeleteUser()
    {
        // Arrange
        Guid requestedBy = ApplicationUserHandlerTestData.GetValidUserId();
        UserRole applicantRole = ApplicationUserHandlerTestData.GetValidUserRole();
        var command = new DeleteUserCommand(Guid.NewGuid(), requestedBy, applicantRole);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _UserRepository.Received(1).DeleteAsync(command.Id, requestedBy, applicantRole, Arg.Any<CancellationToken>());
    }
}