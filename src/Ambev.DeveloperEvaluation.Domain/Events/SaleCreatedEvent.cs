using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event that is raised when a sale is created.
/// </summary>
/// <param name="SaleId"></param>
[ExcludeFromCodeCoverage]
public record SaleCreatedEvent(Guid SaleId)
{
}