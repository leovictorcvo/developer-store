using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class DeleteCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly DeleteCartHandler _handler;

    public DeleteCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _handler = new DeleteCartHandler(_cartRepository);
    }

    [Fact(DisplayName = "Given valid command When handle Then delete cart")]
    public async Task Handle_ValidRequest_ShouldDeleteCart()
    {
        var requestBy = ApplicationCartsTestData.GetValidResourceIdentifier();
        var applicantRole = ApplicationCartsTestData.GetValidUserRole();

        // Arrange
        var command = new DeleteCartCommand(Guid.NewGuid(), requestBy, applicantRole);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _cartRepository.Received(1).DeleteAsync(command.Id, requestBy, applicantRole, Arg.Any<CancellationToken>());
    }
}