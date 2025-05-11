using Ambev.DeveloperEvaluation.Domain.ValueObjects.Sale;

namespace Ambev.DeveloperEvaluation.Domain.Extensions;

public static class SaleItemExtensions
{
    public static decimal CalculateDiscount(this SaleItem item, decimal total)
    {
        var discountPercentage = item.Quantity switch
        {
            < 4 => 0.0m,
            < 10 => 10.0m,
            _ => 15.0m
            //It is not necessary to check if the quantity of items is greater, as the sales item constructor already performs this check.
        };

        return Math.Round(total * (discountPercentage / 100), 2);
    }
}