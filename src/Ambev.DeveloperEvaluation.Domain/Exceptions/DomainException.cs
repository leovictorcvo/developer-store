using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class DomainException : Exception
{
    public string Title { get; }
    public string Detail { get; }

    public DomainException(string title, string detail)
        : base(detail) // Passando o detalhe para a classe base Exception
    {
        Title = title;
        Detail = detail;
    }

    public DomainException(string title, string detail, Exception innerException)
        : base(detail, innerException)
    {
        Title = title;
        Detail = detail;
    }
}