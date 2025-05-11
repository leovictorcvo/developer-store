using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Base class for paginated requests.
/// </summary>
public abstract class PaginatedRequest
{
    /// <summary>
    /// Page number of pagination.
    /// </summary>
    [FromQuery(Name = "_page")]
    public int? Page { get; init; } = 1;

    /// <summary>
    /// Page size of pagination.
    /// </summary>
    [FromQuery(Name = "_size")]
    public int? Size { get; init; } = 10;

    /// <summary>
    /// Ordering of pagination.
    /// </summary>
    [FromQuery(Name = "_order")]
    public string? Order { get; init; }

    /// <summary>
    /// Query filters
    ///
    /// - For numeric or date properties, you can use the prefixes _min (to set the minimum value) and _max (to set the maximum value).
    ///
    ///     - Example: _minPrice=100 or _maxQuantity=10
    ///
    /// - For text properties, values can be filtered using '*' as a wildcard. The search will ignore case sensitivity.
    ///
    ///     - Example: description=Tes* (Starts with 'Tes') or description=*ion (Ends with 'ion') or description=*arm* (Contains 'arm' in the value)
    /// </summary>

    [FromQuery]
    public Dictionary<string, string>? Filters { get; init; } = [];

    /// <summary>
    ///
    /// </summary>
    /// <param name="requestQuery"></param>
    public void ApplyFilters(IQueryCollection requestQuery)
    {
        string[] knowQueryKeys = { "_page", "_size", "_order" };
        foreach (var queryItem in requestQuery)
        {
            if (!knowQueryKeys.Contains(queryItem.Key))
            {
                Filters!.Add(queryItem.Key, queryItem.Value!);
            }
        }
    }
}