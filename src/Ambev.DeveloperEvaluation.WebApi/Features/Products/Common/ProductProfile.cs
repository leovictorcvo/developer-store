using Ambev.DeveloperEvaluation.Application.Products.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;

/// <summary>
/// Profile for mapping between Application and API Product requests
/// </summary>
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ApplicationProductResult, ProductResponse>();
    }
}