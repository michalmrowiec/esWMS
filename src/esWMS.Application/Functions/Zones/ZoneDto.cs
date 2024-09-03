using esWMS.Application.Functions.Warehouses;

namespace esWMS.Application.Functions.Zones
{
    public class ZoneDto
    {
        public string ZoneId { get; set; } = null!;
        public string? ZoneName { get; set; }
        public char ZoneAlias { get; set; }
        public string WarehouseId { get; set; } = null!;
        public decimal? AvgTemperature { get; set; }

        public FlatWarehouseDto? Warehouse { get; set; }
    }
}
