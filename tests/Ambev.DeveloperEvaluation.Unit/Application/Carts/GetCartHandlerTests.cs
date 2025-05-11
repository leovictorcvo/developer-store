using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class GetCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly GetCartHandler _handler;

    public GetCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetCartHandler(_cartRepository, _mapper);
    }

    [Fact(DisplayName = "Given invalid cart identifier throws ResourceNotFoundException")]
    public async Task Handle_InvalidRequest_ThrowsResourceNotFoundException()
    {
        // Given
        var requestBy = ApplicationCartsTestData.GetValidResourceIdentifier();
        var applicantRole = ApplicationCartsTestData.GetValidUserRole();
        var command = new GetCartCommand(Guid.Empty, requestBy, applicantRole);
        _cartRepository.GetByIdAsync(command.Id, Arg.Any<bool>(), requestBy, applicantRole, Arg.Any<CancellationToken>())
            .ThrowsAsync(new ResourceNotFoundException("Cart not found", $"Cart with ID '{command.Id}' not found"));

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ResourceNotFoundException>();
    }

    [Fact(DisplayName = "Given valid cart identifier returns cart")]
    public async Task Handle_ValidRequest_ReturnsCart()
    {
        var requestBy = ApplicationCartsTestData.GetValidResourceIdentifier();
        var applicantRole = ApplicationCartsTestData.GetValidUserRole();
        Cart cart = ApplicationCartsTestData.GetValidCart();
        ApplicationCartResult? result = null;
        // Given
        var command = new GetCartCommand(Guid.Empty, requestBy, applicantRole);
        _cartRepository.GetByIdAsync(command.Id, Arg.Any<bool>(), requestBy, applicantRole, Arg.Any<CancellationToken>()).Returns(Task.FromResult(cart));
        _mapper.Map<ApplicationCartResult>(cart).Returns(ApplicationCartsTestData.GetApplicationCartResultFromCart(cart));

        // When
        var method = async () => result = await _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().NotThrowAsync();
        result.Should().NotBeNull();
        result!.UserId.Should().Be(cart.UserId);
        result!.Date.Should().Be(cart.Date);
        result!.Products.Should().NotBeNullOrEmpty();
        result!.Products.Should().HaveCount(cart.Products.Count);
    }
}