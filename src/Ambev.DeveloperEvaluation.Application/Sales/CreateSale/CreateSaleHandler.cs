using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Builder;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, ApplicationSaleResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IEventNotification _eventNotifier;

    public CreateSaleHandler(ICartRepository cartRepository, IProductRepository productRepository, IUserRepository userRepository, ISaleRepository saleRepository, IMapper mapper, IEventNotification eventNotifier)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _saleRepository = saleRepository;
        _mapper = mapper;
        _eventNotifier = eventNotifier;
    }

    public async Task<ApplicationSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var customer = await _userRepository.GetByIdAsync(command.CustomerId, asNoTracking: true, command.RequestedBy, command.ApplicantRole, cancellationToken);
        var cart = await _cartRepository.GetByIdAsync(command.CartId, asNoTracking: true, command.RequestedBy, command.ApplicantRole, cancellationToken);
        if (cart.UserId != command.CustomerId)
        {
            throw new InvalidOperationException("Customer does not own the cart");
        }

        var saleBuilder = SaleBuilder.Empty()
            .WithId(Guid.NewGuid())
            .WithCreatedAt(DateTime.UtcNow)
            .WithCustomer(c => c
                .WithId(customer.Id)
                .WithName(customer.Name.FullName)
            )
            .WithBranch(command.Branch);

        foreach (var cartItem in cart.Products)
        {
            var product = await _productRepository.GetByIdAsync(cartItem.ProductId, asNoTracking: true, cancellationToken);
            var item = SaleItemBuilder.Empty()
                .WithId(Guid.NewGuid())
                .WithProduct(p => p
                    .WithId(product.Id)
                    .WithName(product.Title)
                    .WithUnitPrice(product.Price)
                 )
                .WithIsCancelled(false)
                .WithQuantity(cartItem.Quantity)
                .Build();
            saleBuilder.AddItem(item);
        }

        var sale = saleBuilder.Build();

        await _saleRepository.AddAsync(sale, cancellationToken);

        await _eventNotifier.NotifyAsync(new SaleCreatedEvent(sale.Id));

        return _mapper.Map<ApplicationSaleResult>(sale);
    }
}