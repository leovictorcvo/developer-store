using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Represents a standard API response.
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Indicates whether the API call was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The status code of the response.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The error code, if any.
    /// </summary>
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
}