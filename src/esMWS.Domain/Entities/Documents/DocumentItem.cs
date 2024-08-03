using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esMWS.Domain.Entities.Documents
{
    public class DocumentItem
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
        public string? WarehouseUnitId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public BaseDocument? Document { get; set; }
        public Product? Product { get; set; }
        public WarehouseUnitItem? WarehouseUnitItem { get; set; }
        public WarehouseUnit? WarehouseUnit { get; set; }
    }
}
