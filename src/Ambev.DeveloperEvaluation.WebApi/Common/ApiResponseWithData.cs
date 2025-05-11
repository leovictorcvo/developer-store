namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Represents a generic API response with data.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResponseWithData<T> : ApiResponse
{
    /// <summary>
    /// The data returned in the response.
    /// </summary>
    public T? Data { get; set; }
}