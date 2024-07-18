using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class WarehouseUnitItems
    {
        [Required]
        [MaxLength(450)]
        public string WarehouseUnitItemId { get; set; } = null!;
        [Required]
        public string WarehouseUnitId { get; set; } = null!;
        [Required]
        public string ProductId { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
        public DateTime BestBefore { get; set; }
        [MaxLength(50)]
        [DisplayName("Batch/Lot")]
        public string? BatchLot { get; set; }
        [MaxLength(100)]
        public string? SerialNumber { get; set; }
        public decimal? Price { get; set; }

        public WarehouseUnit? WarehouseUnit { get; set; }
        public Product? Product { get; set; }
    }
}
