using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Handler for creating a new cart.
/// </summary>
public class CreateCartHandler : IRequestHandler<CreateCartCommand, ApplicationCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the CreateCartHandler class.
    /// </summary>
    /// <param name="cartRepository"></param>
    /// <param name="mapper"></param>
    /// <param name="productRepository"></param>
    /// <param name="userRepository"></param>
    public CreateCartHandler(ICartRepository cartRepository, IMapper mapper, IProductRepository productRepository, IUserRepository userRepository)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the creation of a new cart.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApplicationCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var cart = new Cart();
        cart.Create(command.UserId, command.Date, command.Products);
        await cart.ValidateReferencesAsync(_userRepository, _productRepository, cancellationToken);
        var createdCart = await _cartRepository.CreateAsync(cart, command.RequestedBy, command.ApplicantRole, cancellationToken);
        return _mapper.Map<ApplicationCartResult>(createdCart);
    }
}