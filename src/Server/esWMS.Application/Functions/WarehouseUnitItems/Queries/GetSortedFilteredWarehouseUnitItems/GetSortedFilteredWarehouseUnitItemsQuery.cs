using esWMS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.WarehouseUnitItems.Queries.GetSortedFilteredWarehouseUnitItems
{
    public record GetSortedFilteredWarehouseUnitItemsQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<WarehouseUnitItemDto>>>;
}
