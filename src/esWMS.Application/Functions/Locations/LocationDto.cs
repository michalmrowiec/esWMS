using esWMS.Application.Functions.Zones;

namespace esWMS.Application.Functions.Locations
{
    public class LocationDto
    {
        public string LocationId { get; set; } = null!;
        public string ZoneId { get; set; } = null!;
        public int Row { get; set; }
        public char Column { get; set; }
        public int Level { get; set; }
        public int Cell { get; set; }
        public decimal Capacity { get; set; }
        public decimal? MaxLength { get; set; }
        public decimal? MaxWidth { get; set; }
        public decimal? MaxHeight { get; set; }
        public decimal? MaxWeight { get; set; }
        public bool IsBusy { get; set; }
        public string? DefaultMediaTypeId { get; set; }

        public ZoneDto? Zone { get; set; }
    }
}
