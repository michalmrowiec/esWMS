using esWMS.Client.ViewModels.WarehouseEnviroment.Warehouse;

namespace esWMS.Client.ViewModels.Documents
{
    public class MmpDetailsVM : MmpVM
    {
    }

    public class MmpVM : BaseDocumentVM
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string FromWarehouseId { get; set; } = null!;
        public string RelatedMmmId { get; set; } = null!;

        public WarehouseVM? FromWarehouse { get; set; }
        public MmmVM? RelatedMmm { get; set; }
    }
}
