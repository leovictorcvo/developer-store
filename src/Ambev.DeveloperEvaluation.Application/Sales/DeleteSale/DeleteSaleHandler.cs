using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, Unit>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IEventNotification _eventNotifier;

    public DeleteSaleHandler(ISaleRepository saleRepository, IEventNotification eventNotifier)
    {
        _saleRepository = saleRepository;
        _eventNotifier = eventNotifier;
    }

    public async Task<Unit> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await _saleRepository.DeleteAsync(command.SaleId, command.RequestedBy, command.ApplicantRole, cancellationToken);
        await _eventNotifier.NotifyAsync(new SaleCancelledEvent(command.SaleId));
        return Unit.Value;
    }
}