using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts;

/// <summary>
/// Command to retrieve a paginated list of carts.
/// </summary>
[ExcludeFromCodeCoverage]
public class GetCartsCommand : PaginationCommand, IRequest<PaginationResult<ApplicationCartResult>>
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