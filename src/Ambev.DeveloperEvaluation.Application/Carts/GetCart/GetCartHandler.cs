using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Handler for retrieving a cart by its ID
/// </summary>
public class GetCartHandler : IRequestHandler<GetCartCommand, ApplicationCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the GetCartHandler class
    /// </summary>
    /// <param name="cartRepository"></param>
    /// <param name="mapper"></param>
    public GetCartHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCartCommand to retrieve a cart by its ID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task<ApplicationCartResult> Handle(GetCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(request.Id, asNoTracking: true, request.RequestedBy, request.ApplicantRole, cancellationToken);
        return _mapper.Map<ApplicationCartResult>(cart);
    }
}