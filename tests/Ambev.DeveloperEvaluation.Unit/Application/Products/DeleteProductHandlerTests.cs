using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="DeleteProductHandler"/> class.
/// </summary>
public class DeleteProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly DeleteProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public DeleteProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new DeleteProductHandler(_productRepository);
    }

    /// <summary>
    /// Tests that an invalid request when try delete product throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid product identifier When try delete product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new DeleteProductCommand(Guid.Empty);

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that an valid request when try to delete product not existing in database should throws a exception.
    /// </summary>
    [Fact(DisplayName = "Given valid product identifier When try delete not existing product Then should throws exception")]
    public async Task Handle_NotFoundProduct_ThrowsNotFoundDomainException()
    {
        // Given
        var productId = Guid.NewGuid();
        var command = new DeleteProductCommand(productId);

        _productRepository.DeleteAsync(productId, Arg.Any<CancellationToken>())
            .ThrowsAsync(new ResourceNotFoundException("Product not found", $"The product with ID '{productId}' not found"));

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should()
            .ThrowAsync<ResourceNotFoundException>()
            .WithMessage($"The product with ID '{productId}' not found");
    }

    /// <summary>
    /// Tests that an valid request when delete product should return true to indicates a success operation.
    /// </summary>
    [Fact(DisplayName = "Given valid product identifier When delete product Then should return true to indicates a success operation")]
    public async Task Handle_FoundProduct_Should_Removed_It()
    {
        // Given
        var productId = Guid.NewGuid();
        var command = new DeleteProductCommand(productId);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
    }
}