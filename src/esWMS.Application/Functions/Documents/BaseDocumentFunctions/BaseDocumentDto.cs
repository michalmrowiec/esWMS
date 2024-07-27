using esWMS.Application.Functions.Documents.DocumentItemsFunctions;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions
{
    public abstract class BaseDocumentDto
    {
        public string DocumentId { get; set; } = null!;
        public string IssueWarehouseId { get; set; } = null!;
        public string? Comment { get; set; }
        public DateTime DocumentIssueDate { get; set; }
        public string? IssuingEmployeeId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? AprovedDate { get; set; }
        public string? ApprovingEmployeeId { get; set; }

        public IList<DocumentItemDto> DocumentItems { get; set; } = [];
    }
}
