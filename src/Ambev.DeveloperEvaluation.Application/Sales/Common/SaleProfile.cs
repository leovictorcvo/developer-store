using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

[ExcludeFromCodeCoverage]
public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<Domain.Entities.Sale, ApplicationSaleResult>();
        CreateMap<Domain.ValueObjects.Sale.SaleItem, SaleItem>();
        CreateMap<Domain.ValueObjects.Sale.SaleItemProduct, SaleItemProduct>();
        CreateMap<Domain.ValueObjects.Sale.SaleCustomer, SaleCustomer>();
    }
}