namespace esWMS.Application.Functions.WarehouseUnits.Commands
{
    public class CommonWarehouseUnitCommand
    {
        public decimal? TotalWeight { get; set; }
        public decimal? TotalLength { get; set; }
        public decimal? TotalWidth { get; set; }
        public decimal? TotalHeight { get; set; }
        public bool? CanBeStacked { get; set; }
    }
}
