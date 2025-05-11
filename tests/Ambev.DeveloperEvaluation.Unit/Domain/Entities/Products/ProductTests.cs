using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Products;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Products;

public class ProductTests
{
    [Fact(DisplayName = "Valid product should created successfully")]
    public void Given_ValidUserName_When_Creating_Then_ShouldNotHaveErrors()
    {
        var product = ProductTestData.GenerateValid();

        var newProduct = new Product();
        var method = () => newProduct.Create(
            product.Title,
            product.Price,
            product.Description,
            product.Category,
            product.Image,
            product.Rating
            );

        method.Should().NotThrow();
    }

    [Fact(DisplayName = "Invalid product should throw Validation Exception when creating")]
    public void Given_InvalidUserName_When_Creating_Then_ShouldThrowValidationException()
    {
        var product = ProductTestData.GenerateValid();

        var newProduct = new Product();
        var method = () => newProduct.Create(
            string.Empty,
            product.Price,
            product.Description,
            product.Category,
            product.Image,
            product.Rating
            );

        method.Should().Throw<ValidationException>();
    }

    [Fact(DisplayName = "Valid product should updated successfully")]
    public void Given_ValidUserName_When_Updating_Then_ShouldNotHaveErrors()
    {
        var product = ProductTestData.GenerateValid();

        var newProduct = new Product();
        var method = () => newProduct.Update(
            product.Id,
            product.Title,
            product.Price,
            product.Description,
            product.Category,
            product.Image,
            product.Rating
            );

        method.Should().NotThrow();
    }

    [Fact(DisplayName = "Invalid product should throw Validation Exception when updating")]
    public void Given_InvalidUserName_When_Updating_Then_ShouldThrowValidationException()
    {
        var product = ProductTestData.GenerateValid();

        var newProduct = new Product();
        var method = () => newProduct.Update(
            Guid.Empty,
            product.Title,
            product.Price,
            product.Description,
            product.Category,
            product.Image,
            product.Rating
            );

        method.Should().Throw<ValidationException>();
    }
}