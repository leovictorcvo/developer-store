using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Builder;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

internal class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, ApplicationSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IEventNotification _eventNotifier;

    public CancelSaleItemHandler(IMapper mapper, ISaleRepository saleRepository, IEventNotification eventNotifier)
    {
        _mapper = mapper;
        _saleRepository = saleRepository;
        _eventNotifier = eventNotifier;
    }

    public async Task<ApplicationSaleResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        var savedSale = await _saleRepository.GetByIdAsync(request.SaleId, request.RequestedBy, request.ApplicantRole, cancellationToken);

        _ = savedSale.Items.FirstOrDefault(x => x.Id == request.ItemId) ??
            throw new InvalidOperationException($"Item with id '{request.ItemId}' not found in sale with id '{request.SaleId}'");

        var saleBuilder = SaleBuilder.Empty()
            .WithId(savedSale.Id)
            .WithCreatedAt(savedSale.CreatedAt)
            .WithCustomer(c => c
                .WithId(savedSale.Customer!.Id)
                .WithName(savedSale.Customer!.Name)
            )
            .WithBranch(savedSale.Branch);

        foreach (var saleItem in savedSale.Items)
        {
            var item = SaleItemBuilder.Empty()
                .WithId(saleItem.Id)
                .WithProduct(p => p
                    .WithId(saleItem.Product!.Id)
                    .WithName(saleItem.Product!.Name)
                    .WithUnitPrice(saleItem.Product!.UnitPrice)
                 )
                .WithIsCancelled(saleItem.Id == request.ItemId || saleItem.IsCancelled)
                .WithQuantity(saleItem.Quantity)
                .Build();
            saleBuilder.AddItem(item);
        }

        var sale = saleBuilder.Build();

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _eventNotifier.NotifyAsync(new ItemCancelledEvent(request.SaleId, request.ItemId));
        return _mapper.Map<ApplicationSaleResult>(sale);
    }
}