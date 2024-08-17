using esMWS.Domain.Entities.Documents;
using System.ComponentModel;

namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class WarehouseUnitItem
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
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public WarehouseUnit? WarehouseUnit { get; set; }
        public Product? Product { get; set; }
        public IList<DocumentWarehouseUnitItem> DocumentWarehouseUnitItems { get; set; } = [];

        public WarehouseUnitItem()
        { }
        public WarehouseUnitItem(
            string warehouseUnitId,
            string productId,
            int quantity,
            int blockedQuantity,
            DateTime? bestBefore,
            string? batchLot,
            string? serialNumber,
            decimal? price,
            string? createdBy)
        {
            WarehouseUnitItemId = Guid.NewGuid().ToString();
            WarehouseUnitId = warehouseUnitId;
            ProductId = productId;
            Quantity = quantity;
            BlockedQuantity = blockedQuantity;
            BestBefore = bestBefore;
            BatchLot = batchLot;
            SerialNumber = serialNumber;
            Price = price;
            CreatedAt = DateTime.Now;
            CreatedBy = createdBy;
            ModifiedAt = null;
            ModifiedBy = null;
        }
    }
}
