namespace esWMS.Client.ViewModels
{
    public class ZoneVM
    {
        public string ZoneId { get; set; } = null!;
        public string? ZoneName { get; set; }
        public char ZoneAlias { get; set; }
        public string WarehouseId { get; set; } = null!;
        public int? AvgTemperature { get; set; }

        public WarehouseVM? Warehouse { get; set; }
    }
}
