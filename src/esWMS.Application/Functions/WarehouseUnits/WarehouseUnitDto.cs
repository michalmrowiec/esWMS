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
        public string? MediaId { get; set; }
        public string? LocationId { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? TotalLength { get; set; }
        public decimal? TotalWidth { get; set; }
        public decimal? TotalHeight { get; set; }
        public bool IsBlocked { get; set; }
        public bool? CanBeStacked { get; set; }
        public string? StackOnId { get; set; }
    }
}
