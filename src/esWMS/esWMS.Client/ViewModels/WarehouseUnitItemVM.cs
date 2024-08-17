using System.ComponentModel;

namespace esWMS.Client.ViewModels
{
    public class WarehouseUnitItemVM
    {
        public string WarehouseUnitItemId { get; set; } = null!;
        public string WarehouseUnitId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
        public int BlockedQuantity { get; set; }
        public DateTime? BestBefore { get; set; }
        [DisplayName("Batch/Lot")]
        public string? BatchLot { get; set; }
        public string? SerialNumber { get; set; }
        public decimal? Price { get; set; }

        public WarehouseUnitVM? WarehouseUnit { get; set; }
    }
}