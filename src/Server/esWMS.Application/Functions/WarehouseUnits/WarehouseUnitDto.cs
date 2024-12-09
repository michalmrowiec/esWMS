using esWMS.Application.Functions.Locations;
using esWMS.Application.Functions.WarehouseUnitItems;

namespace esWMS.Application.Functions.WarehouseUnits
{
    public class WarehouseUnitDto : FlatWarehouseUnitDto
    {
        public LocationDto? Location { get; set; }
        public IList<WarehouseUnitItemDto> WarehouseUnitItems { get; set; } = [];
    }

    public class FlatWarehouseUnitDto
    {
        public string WarehouseUnitId { get; set; } = null!;
        public string WarehouseId { get; set; } = null!;
        public string? LocationId { get; set; }
        public double? TotalWeight { get; set; }
        public double? TotalLength { get; set; }
        public double? TotalWidth { get; set; }
        public double? TotalHeight { get; set; }
        public bool IsBlocked { get; set; }
        public bool? CanBeStacked { get; set; }
        public string? StackOnId { get; set; }
    }
}
