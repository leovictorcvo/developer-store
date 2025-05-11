using Ambev.DeveloperEvaluation.Domain.Builder;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Sales;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Builder;

public class SaleBuilderTests
{
    [Fact(DisplayName = "Valid sale should be created successfully")]
    public void Given_ValidData_When_Creating_Then_ShouldNotHaveErrors()
    {
        Guid saleId = SaleTestData.GenerateValidId();
        DateTime createdAt = SaleTestData.GenerateValidCreatedAt();
        var customer = SaleTestData.GenerateValidCustomer();
        string branch = SaleTestData.GenerateValidBranch();
        var (_, items) = SaleTestData.GenerateValidItems(1);

        var method = () => SaleBuilder
            .Empty()
            .WithId(saleId)
            .WithCreatedAt(createdAt)
            .WithCustomer(c => c.WithId(customer.Id).WithName(customer.Name))
            .WithBranch(branch)
            .AddItem(items[0])
            .Build();

        method.Should().NotThrow();
    }

    [Fact(DisplayName = "Invalid sale data should throw DomainException")]
    public void Given_InvalidData_When_Creating_Then_ShouldThrowValidationException()
    {
        Guid saleId = SaleTestData.GenerateValidId();
        DateTime createdAt = SaleTestData.GenerateValidCreatedAt();
        var customer = SaleTestData.GenerateValidCustomer();
        string branch = SaleTestData.GenerateValidBranch();
        var (_, items) = SaleTestData.GenerateValidItems(1);

        var method = () => SaleBuilder
            .Empty()
            .WithId(saleId)
            .WithCreatedAt(createdAt)
            .WithCustomer(c => c.WithId(customer.Id).WithName(string.Empty))
            .WithBranch(branch)
            .AddItem(items[0])
            .Build();

        method.Should().Throw<DomainException>();
    }
}