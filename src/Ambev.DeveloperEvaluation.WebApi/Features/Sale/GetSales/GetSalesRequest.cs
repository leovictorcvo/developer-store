using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSales;

/// <summary>
/// Sales paginated requests.
/// </summary>
public class GetSalesRequest
{
    /// <summary>
    /// Page number of pagination.
    /// </summary>
    [FromQuery(Name = "_page")]
    public int? Page { get; init; } = 1;

    /// <summary>
    /// Page size of pagination.
    /// </summary>
    [FromQuery(Name = "_size")]
    public int? Size { get; init; } = 10;
}