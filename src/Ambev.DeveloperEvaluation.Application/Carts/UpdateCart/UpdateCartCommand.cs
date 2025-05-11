using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

[ExcludeFromCodeCoverage]
public class UpdateCartCommand : ApplicationCartCommand, IRequest<ApplicationCartResult>
{
    /// <summary>
    /// The unique identifier of the cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the user who requested the cart creation.
    /// </summary>
    public Guid RequestedBy { get; set; }

    /// <summary>
    /// The role of the user who requested the cart creation.
    /// </summary>
    public UserRole ApplicantRole { get; set; }
}