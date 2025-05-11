using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class UpdateSaleHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IEventNotification _eventNotifier;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _eventNotifier = Substitute.For<IEventNotification>();

        _handler = new UpdateSaleHandler(_cartRepository, _productRepository, _userRepository, _saleRepository, _mapper, _eventNotifier);
    }

    [Fact(DisplayName = "Given invalid command When validate Then result validation errors")]
    public async Task Handle_InvalidRequest_ReturnValidationError()
    {
        // Given
        var command = new UpdateSaleCommand();

        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<Exception>();
    }

    [Fact(DisplayName = "Given invalid customerId in command throws ResourceNotFoundException")]
    public async Task Handle_InvalidProductIdInCommand_ThrowsResourceNotFoundException()
    {
        ApplicationSaleResult? SaleResult = null;
        // Given
        var command = ApplicationSaleTestData.GetValidUpdateSaleCommand();
        _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(ApplicationSaleTestData.GetValidSale()));
        _userRepository.GetByIdAsync(command.CustomerId, true, Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new ResourceNotFoundException("User not found", $"User with ID '{command.CustomerId}' not found"));

        //When
        var method = async () => SaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ResourceNotFoundException>();
    }

    [Fact(DisplayName = "Given invalid cartId in command throws ResourceNotFoundException")]
    public async Task Handle_InvalidCartIdInCommand_ThrowsResourceNotFoundException()
    {
        ApplicationSaleResult? SaleResult = null;
        // Given
        var command = ApplicationSaleTestData.GetValidUpdateSaleCommand();
        _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(ApplicationSaleTestData.GetValidSale()));
        _cartRepository.GetByIdAsync(command.CartId, true, Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new ResourceNotFoundException("Cart not found", $"Cart with ID '{command.CustomerId}' not found"));

        //When
        var method = async () => SaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ResourceNotFoundException>();
    }

    [Fact(DisplayName = "Given valid command but with diferent customer id from cart Id throws an InvalidOperationException")]
    public async Task Handle_CartCustomerIdDifferentFromCommandCustomer_ThrowsInvalidOperationException()
    {
        ApplicationSaleResult? SaleResult = null;
        // Given
        var cart = ApplicationCartsTestData.GetValidCart();
        var command = ApplicationSaleTestData.GetValidUpdateSaleCommand();
        _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(ApplicationSaleTestData.GetValidSale()));
        _cartRepository.GetByIdAsync(command.CartId, true, Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Cart>(cart));

        //When
        var method = async () => SaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Customer does not own the cart");
    }

    [Fact(DisplayName = "Given valid command Updates a new Sale")]
    public async Task Handle_ValidRequest_UpdatesASale()
    {
        ApplicationSaleResult? SaleResult = null;
        // Given
        var cart = ApplicationCartsTestData.GetValidCart();
        var customer = ApplicationSaleTestData.GetValidCustomer(cart.UserId);
        var command = ApplicationSaleTestData.GetValidUpdateSaleCommand();
        command.CustomerId = cart.UserId;
        _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(ApplicationSaleTestData.GetValidSale()));
        _userRepository.GetByIdAsync(command.CustomerId, true, Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(customer));
        _cartRepository.GetByIdAsync(command.CartId, true, Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Cart>(cart));
        _productRepository.GetByIdAsync(Arg.Any<Guid>(), asNoTracking: true, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(CreateProductHandlerTestData.GetValidProduct()));

        //When
        var method = async () => SaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().NotThrowAsync();
        await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(c => c.Customer!.Id == command.CustomerId), Arg.Any<CancellationToken>());
        await _eventNotifier.Received(1).NotifyAsync(Arg.Any<SaleModifiedEvent>());
    }
}