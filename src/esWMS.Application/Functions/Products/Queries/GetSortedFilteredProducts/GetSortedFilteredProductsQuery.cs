using esMWS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts
{
    public record GetSortedFilteredProductsQuery(SieveModel SieveModel) : IRequest<BaseResponse<PagedResult<ProductDto>>>;
}
