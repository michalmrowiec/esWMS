using esWMS.Application.Functions.Documents.DocumentItemsFunctions;
using esWMS.Application.Functions.Products;
using esWMS.Application.Functions.WarehouseUnits;
using System.ComponentModel;

namespace esWMS.Application.Functions.WarehouseUnitItems
{
    public class WarehouseUnitItemDto
    {
        public string WarehouseUnitItemId { get; set; } = null!;
        public string WarehouseUnitId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public bool IsMediaOfWarehouseUnit { get; set; }
        public decimal Quantity { get; set; }
        public decimal BlockedQuantity { get; set; }
        public DateTime? BestBefore { get; set; }
        [DisplayName("Batch/Lot")]
        public string? BatchLot { get; set; }
        public string? SerialNumber { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public string? Unit { get; set; }
        public int? VatRate { get; set; }

        public FlatWarehouseUnitDto? WarehouseUnit { get; set; }
        public ProductDto? Product { get; set; }
        public List<DocumentWarehouseUnitItemDto> DocumentWarehouseUnitItems { get; set; } = [];
    }
}
