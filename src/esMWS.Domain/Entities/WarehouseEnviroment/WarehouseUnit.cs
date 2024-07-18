using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class WarehouseUnit
    {
        [Required]
        [MaxLength(450)]
        public string WarehouseUnitsId { get; set; } = null!;
        [Required]
        public string WarehouseId { get; set; } = null!;
        public string? MediaId { get; set; }
        public string? LocationId { get; set; }
        public int? TotalWeight { get; set; }
        public int? TotalLength { get; set; }
        public int? TotalWidth { get; set; }
        public int? TotalHeight { get; set; }
        public bool? CanBeStacked { get; set; }
        public string? StackOnId { get; set; }

        public Warehouse? Warehouse { get; set; }
        public Product? Media { get; set; }
        public Location? Location { get; set; }
        public WarehouseUnit? StackOn { get; set; }
        public IList<WarehouseUnitItems>? WarehouseUnitItems { get; set; }
    }
}
