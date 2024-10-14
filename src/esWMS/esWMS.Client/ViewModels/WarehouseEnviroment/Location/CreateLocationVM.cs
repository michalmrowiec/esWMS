namespace esWMS.Client.ViewModels.WarehouseEnviroment.Location
{
    public class CreateLocationVM
    {
        public string ZoneId { get; set; }
        public int Row { get; set; } = 1;
        public char? Column { get; set; } = 'A';
        public int Level { get; set; } = 1;
        public int Cell { get; set; } = 1;
        public decimal Capacity { get; set; } = 1;
        public decimal? MaxLength { get; set; }
        public decimal? MaxWidth { get; set; }
        public decimal? MaxHeight { get; set; }
        public decimal? MaxWeight { get; set; }
        public string? DefaultMediaTypeId { get; set; }
    }
}
