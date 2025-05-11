using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesHandler : IRequestHandler<GetSalesCommand, PaginationResult<ApplicationSaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSalesHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<PaginationResult<ApplicationSaleResult>> Handle(GetSalesCommand request, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetAllAsync(request.Page, request.Size, request.RequestedBy, request.ApplicantRole, cancellationToken);
        var totalSales = await _saleRepository.CountAsync(request.RequestedBy, request.ApplicantRole, cancellationToken);

        return new PaginationResult<ApplicationSaleResult>()
        {
            Page = request.Page,
            Size = request.Size,
            TotalItems = totalSales,
            Items = _mapper.Map<List<ApplicationSaleResult>>(sales)
        };
    }
}