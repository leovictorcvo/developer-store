using Ambev.DeveloperEvaluation.Application.Carts.GetCarts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;

/// <summary>
/// AutoMapper profile for mapping between request and response models for the GetCarts operation.
/// </summary>
public class GetCartsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the etCartsProfile class.
    /// </summary>
    public GetCartsProfile()
    {
        CreateMap<GetCartsRequest, GetCartsCommand>()
            .ForMember(dest => dest.Sort, opt => opt.MapFrom(src => src.Order));
    }
}