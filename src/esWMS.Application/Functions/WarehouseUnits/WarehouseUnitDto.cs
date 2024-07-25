using esWMS.Application.Functions.WarehouseUnitItems;
using esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem;

namespace esWMS.Application.Functions.WarehouseUnits
{
    public class WarehouseUnitDto
    {
        public string WarehouseUnitId { get; set; } = null!;
        public string WarehouseId { get; set; } = null!;
        public string? MediaId { get; set; }
        public string? LocationId { get; set; }
        public int? TotalWeight { get; set; }
        public int? TotalLength { get; set; }
        public int? TotalWidth { get; set; }
        public int? TotalHeight { get; set; }
        public bool? CanBeStacked { get; set; }
        public string? StackOnId { get; set; }

        public IList<WarehouseUnitItemDto> WarehouseUnitItems { get; set; } = [];
    }
}
