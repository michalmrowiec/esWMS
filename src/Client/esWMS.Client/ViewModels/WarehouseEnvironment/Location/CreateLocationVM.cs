namespace esWMS.Client.ViewModels.WarehouseEnvironment.Location
{
    public class CreateLocationVM
    {
        public string ZoneId { get; set; }
        public int Row { get; set; } = 1;
        public char? Column { get; set; } = 'A';
        public int Level { get; set; } = 1;
        public int Cell { get; set; } = 1;
        public decimal Capacity { get; set; } = 1;
        public double? MaxLength { get; set; }
        public double? MaxWidth { get; set; }
        public double? MaxHeight { get; set; }
        public double? MaxWeight { get; set; }
        public string? DefaultMediaTypeId { get; set; }
    }
}
