using esWMS.Client.ViewModels.Documents;
using System.ComponentModel;

namespace esWMS.Client.ViewModels.WarehouseEnvironment
{
    public class WarehouseUnitItemVM
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

        public WarehouseUnitVM? WarehouseUnit { get; set; }
        public ProductVM? Product { get; set; }
        public List<DocumentWarehouseUnitItemVM> DocumentWarehouseUnitItems { get; set; } = [];
    }
}