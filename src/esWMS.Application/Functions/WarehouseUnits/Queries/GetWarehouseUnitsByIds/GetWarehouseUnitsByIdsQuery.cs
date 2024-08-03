using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Queries.GetWarehouseUnitsByIds
{
    public record GetWarehouseUnitsByIdsQuery(string[] WarehouseUniIds)
        : IRequest<BaseResponse<IEnumerable<WarehouseUnitDto>>>;
}
