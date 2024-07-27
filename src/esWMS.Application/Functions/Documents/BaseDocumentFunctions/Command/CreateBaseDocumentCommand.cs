using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command
{
    public abstract class CreateBaseDocumentCommand
    {
        public string IssueWarehouseId { get; set; } = null!;
        public string? Comment { get; set; }
        public DateTime DocumentIssueDate { get; set; }
        public string? IssuingEmployeeId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? AprovedDate { get; set; }
        public string? ApprovingEmployeeId { get; set; }
        public string? CreatedBy { get; set; }

        public IList<CreateDocumentItemCommand> DocumentItems { get; set; } = [];
    }
}
