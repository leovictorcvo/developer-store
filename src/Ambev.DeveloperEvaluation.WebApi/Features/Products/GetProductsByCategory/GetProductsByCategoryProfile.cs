﻿using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;

public class GetProductsByCategoryProfile : Profile
{
    public GetProductsByCategoryProfile()
    {
        CreateMap<GetProductsByCategoryRequest, GetProductsByCategoryCommand>()
            .ForMember(dest => dest.Sort, opt => opt.MapFrom(src => src.Order));
    }
}