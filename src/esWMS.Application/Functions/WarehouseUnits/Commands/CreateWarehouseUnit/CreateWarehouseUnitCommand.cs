using esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit
{
    public class CreateWarehouseUnitCommand
        : CreateFlatWarehouseUnitCommand, IRequest<BaseResponse<WarehouseUnitDto>>
    {
        public IList<CreateWarehouseUnitItemCommand> WarehouseUnitItems { get; set; } = [];
    }

    public class CreateFlatWarehouseUnitCommand
    {
        public string WarehouseId { get; set; } = null!;
        public string? MediaId { get; set; }
        public string? LocationId { get; set; }
        public int? TotalWeight { get; set; }
        public int? TotalLength { get; set; }
        public int? TotalWidth { get; set; }
        public int? TotalHeight { get; set; }
        public bool IsBlocked { get; set; }
        public bool? CanBeStacked { get; set; }
        public string? StackOnId { get; set; }
        public string? CreatedBy { get; set; }
    }
}
