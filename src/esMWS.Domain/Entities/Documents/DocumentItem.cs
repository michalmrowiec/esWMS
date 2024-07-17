using esMWS.Domain.Entities.WarehouseEnviroment;
using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.Documents
{
    public class DocumentItem
    {
        [Required]
        [MaxLength(450)]
        public string DocumentItemsId { get; set; } = null!;
        [Required]
        public string DocumentId { get; set; } = null!;
        [Required]
        public string ProductId { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string ProductCode { get; set; } = null!;
        [MaxLength(100)]
        public string? EanCode { get; set; }
        [Required]
        [MaxLength(250)]
        public string ProductName { get; set; } = null!;
        [Required]
        [Range(0, 1_000_000)]
        public int Quantity { get; set; }
        [Range(0, 10_000_000)]
        public decimal? Price { get; set; }
        [MaxLength(5)]
        public string? Currency { get; set; }
        public string? WarehouseUnitItemId { get; set; }
        [Required]
        public bool IsApproved { get; set; }

        public WarehouseUnitItems? WarehouseUnitItem { get; set; }
    }
}
