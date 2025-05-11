using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;

public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryCommand, PaginationResult<ApplicationProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsByCategoryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PaginationResult<ApplicationProductResult>> Handle(GetProductsByCategoryCommand command, CancellationToken cancellationToken)
    {
        (int totalProducts, List<Product> products) = await _productRepository.GetByCategoryAsync(
            command.CategoryName,
            command.Page,
            command.Size,
            command.Sort,
            cancellationToken);

        return new PaginationResult<ApplicationProductResult>()
        {
            Page = command.Page,
            Size = command.Size,
            TotalItems = totalProducts,
            Items = _mapper.Map<List<ApplicationProductResult>>(products)
        };
    }
}