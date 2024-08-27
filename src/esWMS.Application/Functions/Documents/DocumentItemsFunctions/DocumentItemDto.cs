using esWMS.Application.Functions.WarehouseUnitItems;

namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions
{
    public class DocumentItemDto
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

        public IList<WarehouseUnitItemDto> WarehouseUnitItems { get; set; } = [];
        public IList<DocumentWarehouseUnitItemDto> DocumentWarehouseUnitItems { get; set; } = [];
    }
}
