﻿using esWMS.Domain.Entities.WarehouseEnvironment;
using System.ComponentModel;

namespace esWMS.Domain.Entities.Documents
{
    public class DocumentItem
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
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public BaseDocument? Document { get; set; }
        public Product? Product { get; set; }
        public IList<DocumentWarehouseUnitItem> DocumentWarehouseUnitItems { get; set; } = [];
    }
}
