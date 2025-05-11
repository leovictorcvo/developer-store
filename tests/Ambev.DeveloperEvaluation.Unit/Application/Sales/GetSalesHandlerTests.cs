using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSalesHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSalesHandler _handler;

    public GetSalesHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSalesHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given two sales when handle result should have count two")]
    public async Task Given_Two_Sales_When_Handle_Result_Should_Have_Two()
    {
        // Given
        var command = new GetSalesCommand();

        ICollection<Sale> sales =
        [
            ApplicationSaleTestData.GetValidSale(),
            ApplicationSaleTestData.GetValidSale(),
        ];
        _saleRepository.GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(sales.ToList()));
        var sale1 = sales.ToList()[0];
        var sale2 = sales.ToList()[1];
        _mapper.Map<List<ApplicationSaleResult>>(Arg.Any<List<Sale>>()).Returns(
            [
            ApplicationSaleTestData.GetApplicationSaleResultFromSale(sale1),
            ApplicationSaleTestData.GetApplicationSaleResultFromSale(sale2),
            ]);
        _saleRepository.CountAsync(Arg.Any<Guid>(), Arg.Any<UserRole>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult((long)sales.Count));
        // When
        var queryResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().HaveCount(2);
        queryResult.TotalItems.Should().Be(2);
    }
}