using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Functions.Warehouses;

namespace esWMS.Application.Functions.Documents.MmmFunctions
{
    public class MmmDto : CreateBaseDocumentCommand
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string ToWarehouseId { get; set; } = null!;

        public WarehouseDto? ToWarehouse { get; set; }
    }
}
