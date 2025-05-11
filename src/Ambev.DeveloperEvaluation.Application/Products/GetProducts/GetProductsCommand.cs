using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Products.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

[ExcludeFromCodeCoverage]
public class GetProductsCommand : PaginationCommand, IRequest<PaginationResult<ApplicationProductResult>>
{
}