using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Handler for updating an existing cart.
/// </summary>
public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, ApplicationCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the UpdateCartHandler class.
    /// </summary>
    /// <param name="cartRepository"></param>
    /// <param name="productRepository"></param>
    /// <param name="userRepository"></param>
    /// <param name="mapper"></param>
    public UpdateCartHandler(ICartRepository cartRepository, IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the update of an existing cart.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApplicationCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        var cart = new Cart();
        cart.Update(command.Id, command.UserId, command.Date, command.Products);
        await cart.ValidateReferencesAsync(_userRepository, _productRepository, cancellationToken);
        Cart updatedCart = await _cartRepository.UpdateAsync(cart, command.RequestedBy, command.ApplicantRole, cancellationToken);
        return _mapper.Map<ApplicationCartResult>(updatedCart);
    }
}