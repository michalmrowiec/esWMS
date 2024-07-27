using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esMWS.Domain.Entities.Documents
{
    public class MMP : BaseDocument
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string FromWarehouseId { get; set; } = null!;

        public Warehouse? FromWarehouse { get; set; }
    }
}
