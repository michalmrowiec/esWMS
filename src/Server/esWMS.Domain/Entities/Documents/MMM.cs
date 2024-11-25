using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Domain.Entities.Documents
{
    public class MMM : BaseDocument
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string ToWarehouseId { get; set; } = null!;

        public Warehouse? ToWarehouse { get; set; }
        public MMP? RelatedMmp { get; set; }
    }
}
