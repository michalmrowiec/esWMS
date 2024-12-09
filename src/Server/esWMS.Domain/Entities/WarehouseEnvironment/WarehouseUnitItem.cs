using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Services;
using System.ComponentModel;

namespace esWMS.Domain.Entities.WarehouseEnvironment
{
    public class WarehouseUnitItem
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
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public WarehouseUnit? WarehouseUnit { get; set; }
        public Product? Product { get; set; }
        public List<DocumentWarehouseUnitItem> DocumentWarehouseUnitItems { get; set; } = [];

        public WarehouseUnitItem()
        { }
        public WarehouseUnitItem(
            string warehouseUnitId,
            string productId,
            decimal quantity,
            decimal blockedQuantity,
            DateTime? bestBefore,
            string? batchLot,
            string? serialNumber,
            decimal? price,
            string? createdBy,
            bool isMediaOfWarehouseUnit = false)
        {
            WarehouseUnitItemId = WarehouseUnitIdGenerator.WarehouseUnitItemId();
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
            IsMediaOfWarehouseUnit = isMediaOfWarehouseUnit;
        }
    }
}
