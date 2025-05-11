using Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductCategories
{
    public class GetProductCategoriesProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetProductCategories feature
        /// </summary>
        public GetProductCategoriesProfile()
        {
            CreateMap<GetProductCategoriesResult, GetProductCategoriesResponse>();
        }
    }
}