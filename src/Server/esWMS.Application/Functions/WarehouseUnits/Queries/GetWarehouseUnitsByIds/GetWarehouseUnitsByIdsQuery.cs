using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Queries.GetWarehouseUnitsByIds
{
    public record GetWarehouseUnitsByIdsQuery(params string[] WarehouseUniIds)
        : IRequest<BaseResponse<IEnumerable<WarehouseUnitDto>>>;
}
