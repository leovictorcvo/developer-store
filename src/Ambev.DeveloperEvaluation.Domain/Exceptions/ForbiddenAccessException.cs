using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

/// <summary>
/// Exception thrown when a user tries to access a resource they are not authorized to access.
/// </summary>
[ExcludeFromCodeCoverage]
public class ForbiddenAccessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the ForbiddenAccessException class with a specified error message.
    /// </summary>
    /// <param name="message"></param>
    public ForbiddenAccessException(string message) : base(message)
    {
    }
}