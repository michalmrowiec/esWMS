using esWMS.Application.Functions.Zones;

namespace esWMS.Application.Functions.Warehouses
{
    public class WarehouseDto
    {
        public string WarehouseId { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }

        public IList<ZoneDto> Zones { get; set; } = [];
    }
}
