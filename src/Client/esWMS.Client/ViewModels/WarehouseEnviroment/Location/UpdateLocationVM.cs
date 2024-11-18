namespace esWMS.Client.ViewModels.WarehouseEnvironment.Location
{
    public class UpdateLocationVM
    {
        public string LocationId { get; set; } = null!;
        public decimal Capacity { get; set; }
        public decimal? MaxLength { get; set; }
        public decimal? MaxWidth { get; set; }
        public decimal? MaxHeight { get; set; }
        public decimal? MaxWeight { get; set; }
        public string? DefaultMediaTypeId { get; set; }
    }
}
