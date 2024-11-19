using esWMS.Application.Functions.Documents.BaseDocumentFunctions;

namespace esWMS.Application.Functions.Documents.PwFunctions
{
    public class PwDto : BaseDocumentDto
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string? DepartmentName { get; set; }
    }
}
