using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class CreateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly CreateCartHandler _handler;

    public CreateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();

        _handler = new CreateCartHandler(_cartRepository, _mapper, _productRepository, _userRepository);
    }

    [Fact(DisplayName = "Given invalid command When validate Then result validation errors")]
    public async Task Handle_InvalidRequest_ReturnValidationError()
    {
        // Given
        var command = new CreateCartCommand();

        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given invalid productId in command throws ResourceNotFoundException")]
    public async Task Handle_InvalidProductIdInCommand_ThrowsResourceNotFoundException()
    {
        ApplicationCartResult? cartResult = null;
        // Given
        var command = ApplicationCartsTestData.GetValidCreateCartCommand();
        _productRepository.AllIdsAreValidsAsync(Arg.Any<IEnumerable<Guid>>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult<List<Guid>>([command.Products[0].ProductId]));

        //When
        var method = async () => cartResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ResourceNotFoundException>();
    }

    [Fact(DisplayName = "Given valid command creates a new cart")]
    public async Task Handle_ValidRequest_CreatesACart()
    {
        ApplicationCartResult? cartResult = null;
        // Given
        var command = ApplicationCartsTestData.GetValidCreateCartCommand();
        _productRepository.AllIdsAreValidsAsync(Arg.Any<IEnumerable<Guid>>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult<List<Guid>>([]));
        _mapper.Map<ApplicationCartResult>(Arg.Any<Cart>()).Returns(new ApplicationCartResult
        {
            UserId = command.UserId,
            Date = command.Date,
            Products = command.Products
        });

        //When
        var method = async () => cartResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().NotThrowAsync();
        await _cartRepository.Received(1).CreateAsync(Arg.Is<Cart>(c => c.UserId == command.UserId), Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>());
        cartResult.Should().NotBeNull();
        cartResult!.UserId.Should().Be(command.UserId);
        cartResult.Date.Should().Be(command.Date);
        cartResult.Products.Should().NotBeNullOrEmpty();
        cartResult.Products.Should().HaveCount(command.Products.Count);
    }
}