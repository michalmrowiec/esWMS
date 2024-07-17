using esMWS.Domain.Entities.WarehouseEnviroment;
using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.Documents
{
    public class MMM : DocumentBase
    {
        public DateTime? GoodsReleaseDate { get; set; }
        [Required]
        public string ToWarehouseId { get; set; } = null!;

        public Warehouse? ToWarehouse { get; set; }
    }
}
