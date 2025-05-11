using Ambev.DeveloperEvaluation.Application.Carts.Common;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartRequest, CreateCartCommand>();
        CreateMap<CartRequest, UpdateCartCommand>();
        CreateMap<ApplicationCartResult, CartResponse>();
    }
}