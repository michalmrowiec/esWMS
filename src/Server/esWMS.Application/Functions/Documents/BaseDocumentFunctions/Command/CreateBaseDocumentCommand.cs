using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command
{
    public abstract class CreateBaseDocumentCommand : CreateFlatBaseDocumentCommand
    {
        public IList<CreateDocumentItemCommand> DocumentItems { get; set; } = [];
    }

    public abstract class CreateFlatBaseDocumentCommand
    {
        public string IssueWarehouseId { get; set; } = null!;
        public string? Comment { get; set; }
        public DateTime DocumentIssueDate { get; set; }
        public string? IssuingEmployeeId { get; set; }
        public string? AssignedEmployeeId { get; set; }
        public string? CreatedBy { get; set; }
    }
}
