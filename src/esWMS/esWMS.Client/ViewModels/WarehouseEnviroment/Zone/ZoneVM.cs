using esWMS.Client.ViewModels.WarehouseEnviroment.Location;

namespace esWMS.Client.ViewModels
{
    public class ZoneVM
    {
        public string ZoneId { get; set; } = null!;
        public string? ZoneName { get; set; }
        public char ZoneAlias { get; set; }
        public string WarehouseId { get; set; } = null!;
        public decimal? AvgTemperature { get; set; }

        public WarehouseVM? Warehouse { get; set; }
        public IList<LocationVM> Locations { get; set; } = [];
    }
}
