using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

[ExcludeFromCodeCoverage]
public record CancelSaleItemCommand(Guid SaleId, Guid ItemId, Guid RequestedBy, UserRole ApplicantRole) : IRequest<ApplicationSaleResult>
{
}