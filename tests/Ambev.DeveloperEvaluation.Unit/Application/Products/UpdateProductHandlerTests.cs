using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="UpdateProductHandler"/> class.
/// </summary>
public class UpdateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly UpdateProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public UpdateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateProductHandler(
            _productRepository,
            _mapper);
    }

    /// <summary>
    /// Tests that an invalid request should be return validation errors.
    /// </summary>
    [Fact(DisplayName = "Given invalid command When validate Then result validation errors")]
    public void Handle_InvalidRequest_ReturnValidationError()
    {
        // Given
        var command = new UpdateProductCommand();

        // When
        var validationResult = new ApplicationProductCommandValidator().Validate(command);

        // Then
        validationResult.Should().NotBeNull();
        validationResult.Errors.Should().NotBeEmpty();
    }

    /// <summary>
    /// Tests that an invalid request when try to update product throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When try update product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateProductCommand();

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that an valid request when try to update a no existing product should throws a not found exception.
    /// </summary>
    [Fact(DisplayName = "Given valid product command When try to update and no returns product Then should throws not found exception")]
    public async Task Handle_NotFoundProduct_ThrowsNotFoundDomainException()
    {
        // Given
        var command = UpdateProductHandlerTestData.GenerateValidCommand();
        _productRepository.UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Throws(
            new ResourceNotFoundException("Product not found", $"Product with ID '{command.Id}' not found"));

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should()
            .ThrowAsync<ResourceNotFoundException>()
            .WithMessage($"Product with ID '{command.Id}' not found");
    }

    /// <summary>
    /// Tests that an valid request when try to update a product with existing title should throws a domain exception to notifiy existing product title.
    /// </summary>
    [Fact(DisplayName = "Given valid product command When try to update and return with same title Then should throws domain exception")]
    public async Task Handle_FoundProductWithSameTitle_ThrowsDomainException()
    {
        // Given
        var command = UpdateProductHandlerTestData.GenerateValidCommand();
        var product = UpdateProductHandlerTestData.GenerateProductByCommand(command);

        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product>(product));

        product.Id = Guid.NewGuid(); // note: to simulate another product with same title.
        _productRepository.GetByTitleAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product?>(product));

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage($"Product with title '{command.Title}' already exists");
    }

    /// <summary>
    /// Tests that an valid request when delete product should return true to indicates a success operation.
    /// </summary>
    [Fact(DisplayName = "Given valid product identifier When to update a product Then should return true to indicates a success operation")]
    public async Task Handle_ValidRequest_And_Exists_Category_Should_Returns_Success()
    {
        // Given
        var productId = Guid.NewGuid();
        var command = UpdateProductHandlerTestData.GenerateValidCommand();
        var product = UpdateProductHandlerTestData.GenerateProductByCommand(command);

        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product>(product));

        _mapper.Map<ApplicationProductResult>(Arg.Any<Product>())
            .Returns(new ApplicationProductResult
            {
                Id = productId,
                Description = command.Description,
                Image = command.Image,
                Price = command.Price,
                Rating = command.Rating,
                Title = command.Title,
                Category = command.Category,
            });

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(productId);
    }
}