namespace esWMS.Client.ViewModels.Documents
{
    public class MmmVM : BaseDocumentVM
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string ToWarehouseId { get; set; } = null!;

        public WarehouseVM? ToWarehouse { get; set; }
    }
}
