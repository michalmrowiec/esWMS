namespace esWMS.Client.ViewModels.WarehouseEnvironment.Zone
{
    public class UpdateZoneVM
    {
        public string ZoneId { get; set; } = null!;
        public string? ZoneName { get; set; }
        public double? AvgTemperature { get; set; }
    }
}
