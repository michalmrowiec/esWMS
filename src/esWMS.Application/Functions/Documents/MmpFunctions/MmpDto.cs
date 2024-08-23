using esWMS.Application.Functions.Documents.BaseDocumentFunctions;
using esWMS.Application.Functions.Documents.MmmFunctions;
using esWMS.Application.Functions.Warehouses;

namespace esWMS.Application.Functions.Documents.MmpFunctions
{
    public class MmpDto : FlatMmpDto
    {
        public FlatMmmDto? RelatedMmm { get; set; }
    }

    public class FlatMmpDto : BaseDocumentDto
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string FromWarehouseId { get; set; } = null!;
        public string RelatedMmmId { get; set; } = null!;

        public WarehouseDto? FromWarehouse { get; set; }
    }
}
