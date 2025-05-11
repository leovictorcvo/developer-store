using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.Common;

/// <summary>
/// Profile for mapping between Cart entity and CartResult
/// </summary>
[ExcludeFromCodeCoverage]
public class ApplicationCartProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the CartProfile class.
    /// </summary>
    public ApplicationCartProfile()
    {
        CreateMap<Cart, ApplicationCartResult>();
    }
}