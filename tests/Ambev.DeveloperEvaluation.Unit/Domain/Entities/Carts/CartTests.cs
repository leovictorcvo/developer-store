using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Carts;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Carts
{
    public class CartTests
    {
        [Fact(DisplayName = "Valid cart should be created successfully")]
        public void Given_ValidCart_When_Creating_Then_ShouldNotHaveErrors()
        {
            var cart = CartTestData.GenerateValid();
            var cartItems = CartTestData.GenerateValidListCartItems();

            var newCart = new Cart();
            var method = () => newCart.Create(
                cart.UserId,
                cart.Date,
                [.. cartItems]
                );

            method.Should().NotThrow();
            newCart.UserId.Should().Be(cart.UserId);
            newCart.Date.Should().Be(cart.Date);
            newCart.Products.Should().HaveCount(cartItems.Count);
        }

        [Fact(DisplayName = "Valid cart should be updated successfully")]
        public void Given_ValidCart_When_Updating_Then_ShouldNotHaveErrors()
        {
            var cart = CartTestData.GenerateValid();
            var cartItems = CartTestData.GenerateValidListCartItems();

            var newCart = new Cart();
            newCart.Create(
                cart.UserId,
                cart.Date,
                [.. cartItems]
                );

            var newCartData = CartTestData.GenerateValid();
            var newCartDataId = Guid.NewGuid();
            var method = () => newCart.Update(
                newCartDataId,
                newCartData.UserId,
                newCartData.Date,
                [.. cartItems]
                );

            method.Should().NotThrow();
            newCart.Id.Should().Be(newCartDataId);
            newCart.UserId.Should().Be(newCartData.UserId);
            newCart.Date.Should().Be(newCartData.Date);
            newCart.Products.Should().HaveCount(cartItems.Count);
        }

        [Fact(DisplayName = "Invalid cart should throws ValidationException when creating")]
        public void Given_InvalidCart_When_Creating_Then_ShouldThrowsValidationException()
        {
            var cart = CartTestData.GenerateValid();

            var newCart = new Cart();
            var method = () => newCart.Create(
                cart.UserId,
                cart.Date,
                [.. cart.Products]
                );

            method.Should().Throw<ValidationException>();
        }

        [Fact(DisplayName = "Invalid cart should throws ValidationException when updating")]
        public void Given_ValidCart_When_Updating_Then_ShouldThrowsValidationException()
        {
            var cart = CartTestData.GenerateValid();

            var newCart = new Cart();
            var method = () => newCart.Update(
                cart.Id,
                cart.UserId,
                cart.Date,
                [.. cart.Products]
                );

            method.Should().Throw<ValidationException>();
        }
    }
}