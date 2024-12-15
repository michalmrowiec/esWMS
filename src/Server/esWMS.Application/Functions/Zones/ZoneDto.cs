using esWMS.Application.Functions.Locations;
using esWMS.Application.Functions.Warehouses;

namespace esWMS.Application.Functions.Zones
{
    public class ZoneDto : FlatZoneDto
    {
        public FlatWarehouseDto? Warehouse { get; set; }
        public IList<FlatLocationDto> Locations { get; set; } = [];
    }

    public class FlatZoneDto
    {
        public string ZoneId { get; set; } = null!;
        public string? ZoneName { get; set; }
        public char ZoneAlias { get; set; }
        public string WarehouseId { get; set; } = null!;
        public double? AvgTemperature { get; set; }
    }
}
