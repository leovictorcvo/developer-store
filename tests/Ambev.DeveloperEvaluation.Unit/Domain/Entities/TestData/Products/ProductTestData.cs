using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Products;

public static class ProductTestData
{
    private static readonly Faker<Rating> ratingFaker = new Faker<Rating>()
        .RuleFor(r => r.Rate, f => f.Random.Decimal(min: 1, max: 5))
        .RuleFor(r => r.Count, f => f.Random.Int(min: 0, max: 1000));

    private static readonly Faker<Product> productFaker = new Faker<Product>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(u => u.Title, f => f.Lorem.Sentence(4))
        .RuleFor(u => u.Price, f => f.Random.Decimal(min: 1, max: 2000.00M))
        .RuleFor(u => u.Description, f => f.Lorem.Sentences(2))
        .RuleFor(u => u.Image, f => f.Internet.Url())
        .RuleFor(p => p.Category, f => f.Commerce.ProductMaterial())
        .RuleFor(p => p.Rating, ratingFaker.Generate());

    public static Product GenerateValid()
    {
        return productFaker.Generate();
    }
}