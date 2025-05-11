using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing CreateProductCommand requests
/// </summary>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, ApplicationProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateProductCommand request
    /// </summary>
    /// <param name="command">The CreateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    public async Task<ApplicationProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await new ApplicationProductCommandValidator().ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var productWithThisTitle = await _productRepository.GetByTitleAsync(command.Title, cancellationToken);
        if (productWithThisTitle != null)
        {
            throw new InvalidOperationException($"Product with title '{command.Title}' already exists");
        }

        var newProduct = new Product();
        newProduct.Create(command.Title, command.Price, command.Description, command.Category, command.Image,
            command.Rating);
        var createdProduct = await _productRepository.CreateAsync(newProduct, cancellationToken);
        return _mapper.Map<ApplicationProductResult>(createdProduct);
    }
}