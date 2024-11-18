using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Queries.GetFullWarehouseUnitStack
{
    public record GetFullWarehouseUnitStackQuery(string WarehouseUnitId) : IRequest<BaseResponse<Dictionary<int, WarehouseUnitDto>>>;
}
