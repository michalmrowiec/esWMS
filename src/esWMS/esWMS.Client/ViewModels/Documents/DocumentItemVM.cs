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
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public string? WarehouseUnitItemId { get; set; }
        public bool IsApproved { get; set; }

        public List<DocumentWarehouseUnitItemVM> DocumentWarehouseUnitItems { get; set; } = [];
    }

    public class DocumentWarehouseUnitItemVM
    {
        public DocumentWarehouseUnitItemVM
            (string? documentItemId,
            string? warehouseUnitId,
            int quantity,
            string? warehouseUnitItemId = null)
        {
            DocumentItemId = documentItemId;
            WarehouseUnitId = warehouseUnitId;
            WarehouseUnitItemId = warehouseUnitItemId;
            Quantity = quantity;
        }

        public string? DocumentItemId { get; set; }
        public string? WarehouseUnitId { get; set; }
        public string? WarehouseUnitItemId { get; set; }
        public int Quantity { get; set; }

        public WarehouseUnitItemVM? WarehouseUnitItem { get; set; }

    }
}