using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ApplicationProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of UpdateProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateProductCommand request
    /// </summary>
    /// <param name="command">The UpdateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product details</returns>
    public async Task<ApplicationProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await new UpdateProductCommandValidator().ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var productWithThisTitle = await _productRepository.GetByTitleAsync(command.Title, cancellationToken);
        if (productWithThisTitle != null && productWithThisTitle.Id != command.Id)
        {
            throw new InvalidOperationException($"Product with title '{command.Title}' already exists");
        }
        var productToUpdate = new Product();
        productToUpdate.Update(command.Id, command.Title, command.Price, command.Description, command.Category, command.Image, command.Rating);
        var updatedProduct = await _productRepository.UpdateAsync(productToUpdate, cancellationToken);
        return _mapper.Map<ApplicationProductResult>(updatedProduct);
    }
}