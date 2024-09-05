using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.SetLocationForWarehouseUnit
{
    public class SetLocationForWarehouseUnitCommand : IRequest<BaseResponse<WarehouseUnitDto>>
    {
        public string WarehouseUnitId { get; set; } = null!;
        public string NewLocationId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
