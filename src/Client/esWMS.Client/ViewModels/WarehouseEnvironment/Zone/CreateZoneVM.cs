namespace esWMS.Client.ViewModels.WarehouseEnvironment.Zone
{
    public class CreateZoneVM
    {
        public string? ZoneName { get; set; }
        public char? ZoneAlias { get; set; }
        public string? WarehouseId { get; set; }
        public double? AvgTemperature { get; set; }
    }
}
