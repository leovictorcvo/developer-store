using Ambev.DeveloperEvaluation.Application.Sales.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.Common;

public class SaleResponseProfile : Profile
{
    public SaleResponseProfile()
    {
        CreateMap<ApplicationSaleResult, SaleResponse>();
        CreateMap<SaleCustomer, SaleResponseCustomer>();
        CreateMap<SaleItem, SaleResponseItem>();
        CreateMap<SaleItemProduct, SaleResponseItemProduct>();
    }
}