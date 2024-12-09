﻿using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Domain.Entities.Documents
{
    public class DocumentWarehouseUnitItem
    {
        public string DocumentItemId { get; set; } = null!;
        public string WarehouseUnitItemId { get; set; } = null!;
        public decimal Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public DocumentItem? DocumentItem { get; set; }
        public WarehouseUnitItem? WarehouseUnitItem { get; set; }

    }
}
