using esMWS.Domain.Entities.WarehouseEnviroment;
using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.Documents
{
    public class MMP : DocumentBase
    {
        public DateTime? GoodsReceiptDate { get; set; }
        [Required]
        public string FromWarehouseId { get; set; } = null!;

        public Warehouse? FromWarehouse { get; set; }
    }
}
