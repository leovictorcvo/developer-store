using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Sales;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales;

public class SaleTests
{
    [Fact(DisplayName = "Valid sale should be created successfully")]
    public void Given_ValidSale_When_Creating_Then_ShouldNotHaveErrors()
    {
        Sale? newSale;
        Guid saleId = SaleTestData.GenerateValidId();
        DateTime createdAt = SaleTestData.GenerateValidCreatedAt();
        var customer = SaleTestData.GenerateValidCustomer();
        string branch = SaleTestData.GenerateValidBranch();
        var (totalAmount, items) = SaleTestData.GenerateValidItems();

        var method = () => newSale = new(saleId, createdAt, customer, branch, totalAmount, items);

        method.Should().NotThrow();
    }

    [Fact(DisplayName = "Valid sale must be successfully created with the data provided")]
    public void Given_ValidSale_When_Creating_Then_Values_Should_Be_Equal_Provided()
    {
        Guid saleId = SaleTestData.GenerateValidId();
        DateTime createdAt = SaleTestData.GenerateValidCreatedAt();
        var customer = SaleTestData.GenerateValidCustomer();
        string branch = SaleTestData.GenerateValidBranch();
        var (totalAmount, items) = SaleTestData.GenerateValidItems();

        Sale newSale = new(saleId, createdAt, customer, branch, totalAmount, items);

        newSale.Should().NotBeNull();
        newSale.Id.Should().Be(saleId);
        newSale.CreatedAt.Should().Be(createdAt);
        newSale.Customer.Should().Be(customer);
        newSale.Branch.Should().Be(branch);
        newSale.TotalAmount.Should().Be(totalAmount);
        newSale.Items.Should().HaveCount(items.Count);
        newSale.Items.Should().BeEquivalentTo(items);
    }

    [Fact(DisplayName = "Invalid sale Id should throw exception")]
    public void Given_Invalid_Sale_Id_When_Creating_Then_ShouldThrowException()
    {
        Sale? newSale;
        Guid saleId = Guid.Empty;
        DateTime createdAt = SaleTestData.GenerateValidCreatedAt();
        var customer = SaleTestData.GenerateValidCustomer();
        string branch = SaleTestData.GenerateValidBranch();
        var (totalAmount, items) = SaleTestData.GenerateValidItems();

        var method = () => newSale = new(saleId, createdAt, customer, branch, totalAmount, items);

        method.Should().Throw<ValidationException>();
    }
}