using esWMS.Client.ViewModels.WarehouseEnvironment;
using System.ComponentModel;

namespace esWMS.Client.ViewModels.Documents
{
    public class DocumentItemVM
    {
        public string DocumentItemId { get; set; } = null!;
        public string DocumentId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string ProductCode { get; set; } = null!;
        public string? EanCode { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Quantity { get; set; }
        public DateTime? BestBefore { get; set; }
        [DisplayName("Batch/Lot")]
        public string? BatchLot { get; set; }
        public string? SerialNumber { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public int? VatRate { get; set; }
        public string? Unit { get; set; }
        public string? WarehouseUnitItemId { get; set; }
        public bool IsApproved { get; set; }

        public List<DocumentWarehouseUnitItemVM> DocumentWarehouseUnitItems { get; set; } = [];
    }

    public class DocumentWarehouseUnitItemVM
    {
        public DocumentWarehouseUnitItemVM
            (string? documentItemId,
            string? warehouseUnitId,
            decimal quantity,
            string? warehouseUnitItemId = null,
            bool? isMedia = null)
        {
            DocumentItemId = documentItemId;
            WarehouseUnitId = warehouseUnitId;
            WarehouseUnitItemId = warehouseUnitItemId;
            Quantity = quantity;
            IsMedia = isMedia;
        }

        public string? DocumentWarehouseUnitItemId { get; set; }
        public string? DocumentItemId { get; set; }
        public string? WarehouseUnitId { get; set; }
        public string? WarehouseUnitItemId { get; set; }
        public decimal Quantity { get; set; }
        public bool? IsMedia { get; set; }

        public WarehouseUnitItemVM? WarehouseUnitItem { get; set; }
    }
}