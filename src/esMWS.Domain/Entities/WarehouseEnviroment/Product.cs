using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.SystemActors;
using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class Product
    {
        [Required]
        [MaxLength(450)]
        public string ProductId { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string ProductCode { get; set; } = null!;
        [MaxLength(100)]
        public string? EanCode { get; set; }
        [Required]
        [MaxLength(250)]
        public string ProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        [Required]
        public string CategoryId { get; set; } = null!;
        [MaxLength(10)]
        public string? Unit { get; set; }
        [Required]
        public bool IsWeight { get; set; }
        public int? WeightPerUnit { get; set; }
        public int? StorageTemperature { get; set; }
        [Required]
        public bool IsMedia { get; set; }
        [MaxLength(10)]
        public string? MediaTypeAlias { get; set; }
        public decimal? Price { get; set; }
        public string? SupplierContractorId { get; set; }

        public Category? Category { get; set; }
        public Contractor? SupplierContractor { get; set; }
        public IList<DocumentItem>? DocumentItems { get; set; }
        public IList<WarehouseUnitItem>? WarehouseUnitItems { get; set; }
        public IList<Location>? LocationDefaultMedia { get; set; }
    }
}
