namespace esWMS.Client.ViewModels
{
    public class UpdateZoneVM
    {
        public string ZoneId { get; set; } = null!;
        public string? ZoneName { get; set; }
        public decimal? AvgTemperature { get; set; }
    }
}
