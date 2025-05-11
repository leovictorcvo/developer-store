using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class ResourceNotFoundException : Exception
{
    public string Error { get; }
    public string Detail { get; }

    public ResourceNotFoundException(string error, string detail)
        : base(detail)
    {
        Error = error;
        Detail = detail;
    }

    public ResourceNotFoundException(string error, string detail, Exception innerException)
        : base(detail, innerException)
    {
        Error = error;
        Detail = detail;
    }
}