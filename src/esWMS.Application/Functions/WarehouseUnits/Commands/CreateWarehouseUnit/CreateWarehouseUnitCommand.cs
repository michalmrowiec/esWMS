using esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit
{
    public class CreateWarehouseUnitCommand
        : CommonWarehouseUnitCommand, IRequest<BaseResponse<WarehouseUnitDto>>
    {
        public string WarehouseId { get; set; } = null!;
        public string? LocationId { get; set; }
        public string? CreatedBy { get; set; }
        public IList<CreateWarehouseUnitItemCommand> WarehouseUnitItems { get; set; } = [];
    }
}
