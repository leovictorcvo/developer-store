using Ambev.DeveloperEvaluation.Domain.Builder;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Sales;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Builder;

public class SalaItemProductBuilderTests
{
    [Fact(DisplayName = "Valid item product should be created successfully")]
    public void Given_ValidData_When_Creating_Then_ShouldNotHaveErrors()
    {
        Guid saleId = SaleItemProductTestData.GenerateValidItemProductId();
        string name = SaleItemProductTestData.GenerateValidItemProductName();
        decimal unitPrice = SaleItemProductTestData.GenerateValidItemProductUnitPrice();

        var method = () => SaleItemProductBuilder
            .Empty()
            .WithId(saleId)
            .WithName(name)
            .WithUnitPrice(unitPrice)
            .Build();

        method.Should().NotThrow();
    }

    [Fact(DisplayName = "Invalid item product data should throw DomainException")]
    public void Given_InvalidData_When_Creating_Then_ShouldThrowValidationException()
    {
        Guid saleId = SaleItemProductTestData.GenerateValidItemProductId();
        string name = SaleItemProductTestData.GenerateValidItemProductName();
        decimal unitPrice = 0.0m;

        var method = () => SaleItemProductBuilder
            .Empty()
            .WithId(saleId)
            .WithName(name)
            .WithUnitPrice(unitPrice)
            .Build();

        method.Should().Throw<DomainException>();
    }
}