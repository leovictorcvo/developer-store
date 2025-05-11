using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Command to retrieve a cart by its ID
/// </summary>
[ExcludeFromCodeCoverage]
public record GetCartCommand(Guid Id, Guid RequestedBy, UserRole ApplicantRole) : IRequest<ApplicationCartResult>
{
}