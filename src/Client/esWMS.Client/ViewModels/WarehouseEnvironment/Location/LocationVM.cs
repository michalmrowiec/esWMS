using esWMS.Client.ViewModels.WarehouseEnvironment.Zone;

namespace esWMS.Client.ViewModels.WarehouseEnvironment.Location
{
    public class LocationVM
    {
        public string LocationId { get; set; } = null!;
        public string ZoneId { get; set; } = null!;
        public int Row { get; set; }
        public char Column { get; set; }
        public int Level { get; set; }
        public int Cell { get; set; }
        public decimal Capacity { get; set; }
        public double? MaxLength { get; set; }
        public double? MaxWidth { get; set; }
        public double? MaxHeight { get; set; }
        public double? MaxWeight { get; set; }
        public bool IsFull { get; set; }
        public string? DefaultMediaTypeId { get; set; }

        public ZoneVM? Zone { get; set; }
    }
}
