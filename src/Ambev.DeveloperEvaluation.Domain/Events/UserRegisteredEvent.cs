using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

[ExcludeFromCodeCoverage]
public record UserRegisteredEvent(User user)
{
}