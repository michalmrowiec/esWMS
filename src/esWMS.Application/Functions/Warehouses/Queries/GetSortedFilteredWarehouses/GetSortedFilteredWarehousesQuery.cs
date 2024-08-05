using esMWS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses
{
    public record GetSortedFilteredWarehousesQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<WarehouseDto>>>;
}
