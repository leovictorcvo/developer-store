using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.Common;

[ExcludeFromCodeCoverage]
public class ApplicationProductProfile : Profile
{
    public ApplicationProductProfile()
    {
        CreateMap<Product, ApplicationProductResult>();
    }
}