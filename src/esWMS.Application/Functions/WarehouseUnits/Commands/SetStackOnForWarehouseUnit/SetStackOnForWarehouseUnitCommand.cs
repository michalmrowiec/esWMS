using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.SetStackOnForWarehouseUnit
{
    public class SetStackOnForWarehouseUnitCommand : IRequest<BaseResponse<WarehouseUnitDto>>
    {
        public string WarehouseUnitId { get; set; } = null!;
        public string? StackOnWarehouseUnitId { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
