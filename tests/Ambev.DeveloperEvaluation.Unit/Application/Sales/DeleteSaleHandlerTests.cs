using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IEventNotification _eventNotifier;
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _eventNotifier = Substitute.For<IEventNotification>();
        _handler = new DeleteSaleHandler(_saleRepository, _eventNotifier);
    }

    [Fact(DisplayName = "Given valid command When handle Then delete sale")]
    public async Task Handle_ValidRequest_ShouldDeleteSale()
    {
        // Arrange
        Guid requestedBy = ApplicationUserHandlerTestData.GetValidUserId();
        UserRole applicantRole = ApplicationUserHandlerTestData.GetValidUserRole();
        var command = new DeleteSaleCommand(Guid.NewGuid(), requestedBy, applicantRole);
        _saleRepository.GetByIdAsync(command.SaleId, requestedBy, applicantRole, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(ApplicationSaleTestData.GetValidSale()));
        _saleRepository.DeleteAsync(Arg.Any<Guid>(), requestedBy, applicantRole, Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));
        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), requestedBy, applicantRole, Arg.Any<CancellationToken>());
        await _eventNotifier.Received(1).NotifyAsync(Arg.Any<SaleCancelledEvent>());
    }
}