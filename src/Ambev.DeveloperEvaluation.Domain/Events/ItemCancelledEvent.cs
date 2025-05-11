using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event that is raised when an item is cancelled.
/// </summary>
/// <param name="SaleId"></param>
/// <param name="ItemId"></param>
[ExcludeFromCodeCoverage]
public record ItemCancelledEvent(Guid SaleId, Guid ItemId)
{
}