using esWMS.Application.Functions.Documents.BaseDocumentFunctions;
using esWMS.Application.Functions.Documents.MmpFunctions;
using esWMS.Application.Functions.Warehouses;

namespace esWMS.Application.Functions.Documents.MmmFunctions
{
    public class MmmDto : FlatMmmDto
    {
        public FlatMmpDto? RelatedMmp { get; set; }
    }

    public class FlatMmmDto : BaseDocumentDto
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string ToWarehouseId { get; set; } = null!;

        public WarehouseDto? ToWarehouse { get; set; }
    }
}
