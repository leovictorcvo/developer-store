using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Products;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetProductsByCategoryHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly GetProductsByCategoryHandler _handler;

    public GetProductsByCategoryHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetProductsByCategoryHandler(_productRepository, _mapper);
    }

    [Fact(DisplayName = "Given two products with the category searched for when running the result must have count two")]
    public async Task Given_Two_Products_When_Handle_Result_Should_Have_Two()
    {
        // Given
        var command = new GetProductsByCategoryCommand();
        command.SetProductsCategory("category");

        ICollection<Product> products =
        [
            ProductTestData.GenerateValid(),
            ProductTestData.GenerateValid(),
        ];
        _productRepository.GetByCategoryAsync(command.CategoryName, Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string?>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((products.Count, products.ToList())));
        var product1 = products.ToList()[0];
        var product2 = products.ToList()[1];
        _mapper.Map<List<ApplicationProductResult>>(Arg.Any<List<Product>>()).Returns(
            [
                new ApplicationProductResult
                {
                    Id = product1.Id,
                    Image = product1.Image,
                    Price = product1.Price,
                    Rating = product1.Rating,
                    Title = product1.Title,
                    Category = product1.Category,
                    Description = product1.Description,
                },
                new ApplicationProductResult
                {
                    Id = product2.Id,
                    Image = product2.Image,
                    Price = product2.Price,
                    Rating = product2.Rating,
                    Title = product2.Title,
                    Category = product2.Category,
                    Description = product2.Description,
                }
            ]);

        // When
        var queryResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().HaveCount(2);
        queryResult.TotalItems.Should().Be(2);
    }
}