namespace esWMS.Application.Functions.WarehouseUnits.Commands
{
    public class CommonWarehouseUnitCommand
    {
        public double? TotalWeight { get; set; }
        public double? TotalLength { get; set; }
        public double? TotalWidth { get; set; }
        public double? TotalHeight { get; set; }
        public bool? CanBeStacked { get; set; }
    }
}
