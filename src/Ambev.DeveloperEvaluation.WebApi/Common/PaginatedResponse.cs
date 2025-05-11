namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Represents a paginated response.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedResponse<T> : ApiResponseWithData<IEnumerable<T>>
{
    /// <summary>
    /// The current page number.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public long TotalCount { get; set; }
}