using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Carts.GetCarts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class GetCartsHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly GetCartsHandler _handler;

    public GetCartsHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetCartsHandler(_cartRepository, _mapper);
    }

    [Fact(DisplayName = "Given two carts when handle result should have count two")]
    public async Task Given_Two_Carts_When_Handle_Result_Should_Have_Two()
    {
        // Given
        var command = new GetCartsCommand();

        ICollection<Cart> carts =
        [
            ApplicationCartsTestData.GetValidCart(),
            ApplicationCartsTestData.GetValidCart(),
        ];
        _cartRepository.GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string?>(), Arg.Any<Dictionary<string, string>>(), Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((carts.Count, carts.ToList())));
        var cart1 = carts.ToList()[0];
        var cart2 = carts.ToList()[1];
        _mapper.Map<List<ApplicationCartResult>>(Arg.Any<List<Cart>>()).Returns(
            [
                ApplicationCartsTestData.GetApplicationCartResultFromCart(cart1),
                ApplicationCartsTestData.GetApplicationCartResultFromCart(cart2)
            ]);

        // When
        var queryResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().HaveCount(2);
        queryResult.TotalItems.Should().Be(2);
    }
}