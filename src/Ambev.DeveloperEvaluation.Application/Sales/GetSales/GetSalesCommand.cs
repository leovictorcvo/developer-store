using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

[ExcludeFromCodeCoverage]
public class GetSalesCommand : PaginationCommand, IRequest<PaginationResult<ApplicationSaleResult>>
{
    /// <summary>
    /// The unique identifier of the user who requested the cart creation.
    /// </summary>
    public Guid RequestedBy { get; set; }

    /// <summary>
    /// The role of the user who requested the cart creation.
    /// </summary>
    public UserRole ApplicantRole { get; set; }
}