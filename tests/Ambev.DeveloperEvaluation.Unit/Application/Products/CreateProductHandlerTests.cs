using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="CreateProductHandler"/> class.
/// </summary>
public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductHandler(
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
        var command = new CreateProductCommand();

        // When
        var validationResult = new ApplicationProductCommandValidator().Validate(command);

        // Then
        validationResult.Should().NotBeNull();
        validationResult.Errors.Should().NotBeEmpty();
    }

    /// <summary>
    /// Tests that an invalid request when try to create product throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid request When try create product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateProductCommand();

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given valid product command When try to create and return with same title Then should throws exception")]
    public async Task Handle_FoundProductWithSameTitle_ThrowsException()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        _productRepository.GetByTitleAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product?>(new Product()));

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage($"Product with title '{command.Title}' already exists");
    }

    [Fact(DisplayName = "Given valid product command creates a product")]
    public async Task Handle_ValidCreateCommand_CreateProduct()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        _productRepository.GetByTitleAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product?>(null));

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().NotThrowAsync();
    }
}