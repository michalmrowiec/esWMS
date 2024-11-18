namespace esWMS.Application.Functions.Zones.Commands
{
    public abstract class CommonZoneCommand
    {
        public string? ZoneName { get; set; }
        public decimal? AvgTemperature { get; set; }
    }
}
