using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Represents a command to delete a sale.
/// </summary>
[ExcludeFromCodeCoverage]
public record DeleteSaleCommand(Guid SaleId, Guid RequestedBy, UserRole ApplicantRole) : IRequest<Unit>
{
}