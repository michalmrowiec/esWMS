using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.SetLocationForWarehouseUnit
{
    public class SetLocationForWarehouseUnitCommand : IRequest<BaseResponse<WarehouseUnitDto>>
    {
        public string WarehouseUnitId { get; set; } = null!;
        public string? LocationId { get; set; }
        public bool RemoveFromStack { get; set; } = false;
        public string? ModifiedBy { get; set; }
    }
}
