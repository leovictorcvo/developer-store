using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class GetUsersCommand : PaginationCommand, IRequest<PaginationResult<ApplicationUserResult>>
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