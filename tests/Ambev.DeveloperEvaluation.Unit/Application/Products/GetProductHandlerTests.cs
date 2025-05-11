using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData.Products;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="GetProductHandler"/> class.
/// </summary>
public class GetProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly GetProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public GetProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetProductHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that an invalid request when try retrieving product details throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid product identifier When get product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetProductCommand(Guid.Empty);
        _productRepository.GetByIdAsync(command.Id, Arg.Any<bool>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new ResourceNotFoundException("Product not found", $"The product with ID '{command.Id}' not found"));

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ResourceNotFoundException>();
    }

    /// <summary>
    /// Tests that an valid request when product is not found should throws a not found exception.
    /// </summary>
    [Fact(DisplayName = "Given valid product identifier When product is not found Then should throws not found exception")]
    public async Task Handle_NotFoundProduct_ThrowsNotFoundDomainException()
    {
        // Given
        var productId = Guid.NewGuid();
        var command = new GetProductCommand(productId);
        _productRepository.GetByIdAsync(productId, Arg.Any<bool>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new ResourceNotFoundException("Product not found", $"The product with ID '{productId}' not found"));

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should()
            .ThrowAsync<ResourceNotFoundException>()
            .WithMessage($"The product with ID '{productId}' not found");
    }

    /// <summary>
    /// Tests that product is returned.
    /// </summary>
    [Fact(DisplayName = "Given valid product identifier When get product details Then product is returned")]
    public async Task Given_Product_Details_When_Handle_Result_Should_Return_Product()
    {
        // Given
        var product = ProductTestData.GenerateValid();
        var command = new GetProductCommand(product.Id);

        _mapper.Map<ApplicationProductResult>(Arg.Any<Product>()).Returns(new ApplicationProductResult
        {
            Id = product.Id,
            Image = product.Image,
            Price = product.Price,
            Rating = product.Rating,
            Title = product.Title,
            Category = product.Category,
            Description = product.Description,
        });
        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product>(product));

        // When
        var queryResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        queryResult.Should().NotBeNull();
        queryResult.Id.Should().Be(product.Id);
    }
}