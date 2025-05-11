using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Application.Pagination;

/// <summary>
/// Command used to perform paged queries of registered data
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class PaginationCommand
{
    /// <summary>
    /// Page number of pagination.
    /// </summary>
    public int Page { get; init; }

    /// <summary>
    /// Page size of pagination.
    /// </summary>
    public int Size { get; init; }

    /// <summary>
    /// Ordering of pagination.
    /// </summary>
    public string? Sort { get; init; }

    /// <summary>
    /// Query filters
    /// </summary>
    public Dictionary<string, string>? Filters { get; init; } = [];
}