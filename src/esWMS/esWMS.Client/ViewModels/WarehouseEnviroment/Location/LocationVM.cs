namespace esWMS.Client.ViewModels.WarehouseEnviroment.Location
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
        public decimal? MaxLength { get; set; }
        public decimal? MaxWidth { get; set; }
        public decimal? MaxHeight { get; set; }
        public decimal? MaxWeight { get; set; }
        public bool IsFull { get; set; }
        public string? DefaultMediaTypeId { get; set; }

        public ZoneVM? Zone { get; set; }
    }
}
