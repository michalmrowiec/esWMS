using esWMS.Application.Functions.Documents.BaseDocumentFunctions;

namespace esWMS.Application.Functions.Documents.ZwFunctions
{
    public class ZwDto : BaseDocumentDto
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string? DepartmentName { get; set; }
    }
}
