using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event that is raised when a sale is cancelled.
/// </summary>
[ExcludeFromCodeCoverage]
public record SaleCancelledEvent(Guid SaleId)
{
}