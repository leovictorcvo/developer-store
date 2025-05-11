using Ambev.DeveloperEvaluation.Domain.Extensions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Extensions;

public class SaleItemExtensionTests
{
    [Theory(DisplayName = "Should calculate the discount correctly within the allowed quantity")]
    [InlineData(100.0, 1, 0.0)]
    [InlineData(100.0, 3, 0.0)]
    [InlineData(100.0, 4, 40.0)]
    [InlineData(100.0, 9, 90.0)]
    [InlineData(100.0, 10, 150.0)]
    [InlineData(100.0, 20, 300.0)]
    public void CalculateDiscount_ShouldCalculateCorrectly(decimal unitPrice, int quantity, decimal expectedDiscount)
    {
        // Arrange
        var saleItem = new SaleItem(Guid.NewGuid(), new SaleItemProduct(Guid.NewGuid(), "Test Product", unitPrice), quantity, false);
        decimal total = Math.Round(quantity * unitPrice, 2);
        // Act
        var discount = saleItem.CalculateDiscount(total);
        // Assert
        Assert.Equal(expectedDiscount, discount);
    }
}