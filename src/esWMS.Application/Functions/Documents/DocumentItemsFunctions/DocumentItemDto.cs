using esWMS.Application.Functions.WarehouseUnitItems;
using System.ComponentModel;

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
        public DateTime? BestBefore { get; set; }
        [DisplayName("Batch/Lot")]
        public string? BatchLot { get; set; }
        public string? SerialNumber { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public int? VatRate { get; set; }
        public string? Unit { get; set; }
        public string? WarehouseUnitItemId { get; set; }
        public bool IsApproved { get; set; }

        public IList<WarehouseUnitItemDto> WarehouseUnitItems { get; set; } = [];
        public IList<DocumentWarehouseUnitItemDto> DocumentWarehouseUnitItems { get; set; } = [];
    }
}
