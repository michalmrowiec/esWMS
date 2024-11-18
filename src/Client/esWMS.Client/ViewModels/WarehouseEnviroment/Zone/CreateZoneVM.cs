namespace esWMS.Client.ViewModels
{
    public class CreateZoneVM
    {
        public string? ZoneName { get; set; }
        public char? ZoneAlias { get; set; }
        public string? WarehouseId { get; set; }
        public decimal? AvgTemperature { get; set; }
    }
}
