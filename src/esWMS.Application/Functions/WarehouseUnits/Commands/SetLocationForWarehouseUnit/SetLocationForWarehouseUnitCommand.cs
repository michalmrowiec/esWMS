using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.SetLocationForWarehouseUnit
{
    public record SetLocationForWarehouseUnitCommand
        (string WarehouseUnitId, string NewLocationId)
        : IRequest<BaseResponse<WarehouseUnitDto>>;
}
