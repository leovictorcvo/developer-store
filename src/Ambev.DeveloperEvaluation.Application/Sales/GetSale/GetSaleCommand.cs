using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command to get a sale by its ID.
/// </summary>
[ExcludeFromCodeCoverage]
public record GetSaleCommand(Guid Id, Guid RequestedBy, UserRole ApplicantRole) : IRequest<ApplicationSaleResult>
{
}