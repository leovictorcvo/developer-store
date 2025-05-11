using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event that is raised when a sale is modified.
/// </summary>
/// <param name="SaleId"></param>
[ExcludeFromCodeCoverage]
public record SaleModifiedEvent(Guid SaleId)
{
}