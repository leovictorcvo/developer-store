using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

[ExcludeFromCodeCoverage]
public class UpdateSaleCommand : IRequest<ApplicationSaleResult>
{
    /// <summary>
    /// Gets or sets the sale identifier.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the customer identifier.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the branch identifier.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cart identifier.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// The unique identifier of the user who requested the cart creation.
    /// </summary>
    public Guid RequestedBy { get; set; }

    /// <summary>
    /// The role of the user who requested the cart creation.
    /// </summary>
    public UserRole ApplicantRole { get; set; }
}