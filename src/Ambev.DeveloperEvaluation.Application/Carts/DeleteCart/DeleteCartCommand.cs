using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Command for deleting a cart
/// </summary>
[ExcludeFromCodeCoverage]
public record DeleteCartCommand(Guid Id, Guid RequestedBy, UserRole ApplicantRole) : IRequest<Unit>
{
}