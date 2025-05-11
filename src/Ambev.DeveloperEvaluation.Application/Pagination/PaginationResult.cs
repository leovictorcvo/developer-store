using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Application.Pagination;

[ExcludeFromCodeCoverage]
public class PaginationResult<T>
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
    /// Items of pagination.
    /// </summary>
    public ICollection<T> Items { get; init; } = [];

    /// <summary>
    /// Total items of pagination.
    /// </summary>
    public long TotalItems { get; init; }
}