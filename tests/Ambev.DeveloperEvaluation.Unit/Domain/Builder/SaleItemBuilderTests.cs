using Ambev.DeveloperEvaluation.Domain.Builder;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Sales;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Builder;

public class SaleItemBuilderTests
{
    [Fact(DisplayName = "Valid sale should be created successfully")]
    public void Given_ValidData_When_Creating_Then_ShouldNotHaveErrors()
    {
        Guid saleId = SaleItemTestData.GenerateValidSaleItemId();
        SaleItemProduct product = SaleItemTestData.GenerateValidSaleItemProduct();
        bool isCancelled = SaleItemTestData.GenerateValidSaleItemIsCancelled();
        int quantity = SaleItemTestData.GenerateValidSaleItemQuantity();

        var method = () => SaleItemBuilder
            .Empty()
            .WithId(saleId)
            .WithProduct(p => p.WithId(product.Id).WithName(product.Name).WithUnitPrice(product.UnitPrice))
            .WithIsCancelled(isCancelled)
            .WithQuantity(quantity)
            .Build();

        method.Should().NotThrow();
    }

    [Fact(DisplayName = "Invalid saleItem data should throw DomainException")]
    public void Given_InvalidData_When_Creating_Then_ShouldThrowValidationException()
    {
        Guid saleId = SaleItemTestData.GenerateValidSaleItemId();
        SaleItemProduct product = SaleItemTestData.GenerateValidSaleItemProduct();
        bool isCancelled = SaleItemTestData.GenerateValidSaleItemIsCancelled();

        var method = () => SaleItemBuilder
            .Empty()
            .WithId(saleId)
            .WithProduct(p => p.WithId(product.Id).WithName(product.Name).WithUnitPrice(product.UnitPrice))
            .WithIsCancelled(isCancelled)
            .WithQuantity(21)
            .Build();

        method.Should().Throw<DomainException>();
    }
}