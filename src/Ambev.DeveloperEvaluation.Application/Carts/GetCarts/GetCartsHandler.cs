using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts;

/// <summary>
/// Handler for retrieving a paginated list of carts.
/// </summary>
public class GetCartsHandler : IRequestHandler<GetCartsCommand, PaginationResult<ApplicationCartResult>>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor for GetCartsHandler
    /// </summary>
    /// <param name="cartRepository"></param>
    /// <param name="mapper"></param>
    public GetCartsHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCartsCommand to retrieve a paginated list of carts.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PaginationResult<ApplicationCartResult>> Handle(GetCartsCommand command, CancellationToken cancellationToken)
    {
        (int totalCarts, List<Cart> carts) = await _cartRepository.GetAllAsync(
            command.Page,
            command.Size,
            command.Sort,
            command.Filters,
            command.RequestedBy,
            command.ApplicantRole,
            cancellationToken);

        return new PaginationResult<ApplicationCartResult>()
        {
            Page = command.Page,
            Size = command.Size,
            TotalItems = totalCarts,
            Items = _mapper.Map<List<ApplicationCartResult>>(carts)
        };
    }
}