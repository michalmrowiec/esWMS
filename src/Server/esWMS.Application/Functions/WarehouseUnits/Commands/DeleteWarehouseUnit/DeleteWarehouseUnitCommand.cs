using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.DeleteWarehouseUnit
{
    public record DeleteWarehouseUnitCommand(string WarehouseUnitId) : IRequest<BaseResponse>;
}
