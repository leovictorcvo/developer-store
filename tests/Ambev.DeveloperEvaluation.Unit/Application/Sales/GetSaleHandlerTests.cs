using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given invalid Sale identifier throws ResourceNotFoundException")]
    public async Task Handle_InvalidRequest_ThrowsResourceNotFoundException()
    {
        // Given
        Guid requestedBy = ApplicationUserHandlerTestData.GetValidUserId();
        UserRole applicantRole = ApplicationUserHandlerTestData.GetValidUserRole();
        var command = new GetSaleCommand(Guid.Empty, requestedBy, applicantRole);
        _saleRepository.GetByIdAsync(command.Id, requestedBy, applicantRole, Arg.Any<CancellationToken>())
            .ThrowsAsync(new ResourceNotFoundException("Sale not found", $"Sale with ID '{command.Id}' not found"));

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ResourceNotFoundException>();
    }

    [Fact(DisplayName = "Given valid Sale identifier returns Sale")]
    public async Task Handle_ValidRequest_ReturnsSale()
    {
        Sale sale = ApplicationSaleTestData.GetValidSale();
        ApplicationSaleResult? result = null;
        Guid requestedBy = ApplicationUserHandlerTestData.GetValidUserId();
        UserRole applicantRole = ApplicationUserHandlerTestData.GetValidUserRole();
        var command = new GetSaleCommand(Guid.Empty, requestedBy, applicantRole);

        _saleRepository.GetByIdAsync(command.Id, requestedBy, applicantRole, Arg.Any<CancellationToken>()).Returns(Task.FromResult(sale));
        _mapper.Map<ApplicationSaleResult>(sale).Returns(ApplicationSaleTestData.GetApplicationSaleResultFromSale(sale));

        // When
        var method = async () => result = await _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().NotThrowAsync();
        result.Should().NotBeNull();
    }
}