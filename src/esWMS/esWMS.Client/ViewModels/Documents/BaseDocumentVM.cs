namespace esWMS.Client.ViewModels.Documents
{
    public class BaseDocumentVM
    {
        public string DocumentId { get; set; } = null!;
        public string IssueWarehouseId { get; set; } = null!;
        public string? Comment { get; set; }
        public DateTime DocumentIssueDate { get; set; }
        public string? IssuingEmployeeId { get; set; }
        public string? AssignedEmployeeId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? AprovedDate { get; set; }
        public string? ApprovingEmployeeId { get; set; }

        public IList<DocumentItemVM> DocumentItems { get; set; } = [];
    }
}
