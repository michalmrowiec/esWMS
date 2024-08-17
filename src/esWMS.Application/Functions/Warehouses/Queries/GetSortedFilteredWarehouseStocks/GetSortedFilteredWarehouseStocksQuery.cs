using esMWS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Warehouses.Queries.GetSortedFilteredWarehouseStocks
{
    public record GetSortedFilteredWarehouseStocksQuery(SieveModel SieveModel, string? warehouseId = null)
        : IRequest<BaseResponse<PagedResult<WarehouseStockDto>>>;
}
