using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.UpdateWarehouseUnit
{
    public class UpdateWarehouseUnitCommand
        : CommonWarehouseUnitCommand, IRequest<BaseResponse<WarehouseUnitDto>>
    {
        public string WarehouseUnitId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
