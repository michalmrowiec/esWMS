using esMWS.Domain.Models;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses
{
    public record GetSortedFilteredPzQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<PzDto>>>;
}
