﻿using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.SystemActors;

namespace esWMS.Domain.Entities.WarehouseEnvironment
{
    public class Product
    {
        public string ProductId { get; set; } = null!;
        public string ProductCode { get; set; } = null!;
        public string? EanCode { get; set; }
        public string ProductName { get; set; } = null!;
        public string ShortProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public string CategoryId { get; set; } = null!;
        public string? Unit { get; set; }
        public bool IsWeight { get; set; }
        public double? TotalWeight { get; set; }
        public double? TotalLength { get; set; }
        public double? TotalWidth { get; set; }
        public double? TotalHeight { get; set; }
        public double? MinStorageTemperature { get; set; }
        public double? MaxStorageTemperature { get; set; }
        public bool IsMedia { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public int? VatRate { get; set; }
        public string? SupplierContractorId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public Category? Category { get; set; }
        public Contractor? SupplierContractor { get; set; }
        public IList<DocumentItem> DocumentItems { get; set; } = [];
        public IList<WarehouseUnitItem> WarehouseUnitItems { get; set; } = [];
        public IList<Location> LocationDefaultMedia { get; set; } = [];
    }
}
