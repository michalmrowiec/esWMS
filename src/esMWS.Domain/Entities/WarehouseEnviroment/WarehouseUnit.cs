namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class WarehouseUnit
    {
        public string WarehouseUnitId { get; set; } = null!;
        public string WarehouseId { get; set; } = null!;
        public string? LocationId { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? TotalLength { get; set; }
        public decimal? TotalWidth { get; set; }
        public decimal? TotalHeight { get; set; }
        public bool IsBlocked { get; set; }
        public bool? CanBeStacked { get; set; }
        public string? StackOnId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public Warehouse? Warehouse { get; set; }
        public Location? Location { get; set; }
        public WarehouseUnit? StackOn { get; set; }
        public IList<WarehouseUnitItem> WarehouseUnitItems { get; set; } = [];
    }
}
