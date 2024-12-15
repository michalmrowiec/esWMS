using esWMS.Domain.Entities.SystemActors;
using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Domain.Entities.Documents
{
    public abstract class BaseDocument
    {
        public string DocumentId { get; set; } = null!;
        public string IssueWarehouseId { get; set; } = null!;
        public string? Comment { get; set; }
        public DateTime DocumentIssueDate { get; set; }
        public string? IssuingEmployeeId { get; set; }
        public string? AssignedEmployeeId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? ApprovingEmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public Warehouse? IssueWarehouse { get; set; }
        public Employee? IssuingEmployee { get; set; }
        public Employee? AssignedEmployee { get; set; }
        public Employee? ApprovingEmployee { get; set; }
        public IList<DocumentItem> DocumentItems { get; set; } = [];

        protected BaseDocument()
        { }

        protected BaseDocument
            (string documentId,
            string issueWarehouseId,
            string? comment,
            DateTime documentIssueDate,
            string? issuingEmployeeId,
            string? assignedEmployeeId,
            bool isApproved,
            DateTime? aprovedDate,
            string? approvingEmployeeId,
            DateTime createdAt,
            string? createdBy,
            DateTime? modifiedAt,
            string? modifiedBy)
        {
            DocumentId = documentId;
            IssueWarehouseId = issueWarehouseId;
            Comment = comment;
            DocumentIssueDate = documentIssueDate;
            IssuingEmployeeId = issuingEmployeeId;
            AssignedEmployeeId = assignedEmployeeId;
            IsApproved = isApproved;
            ApprovalDate = aprovedDate;
            ApprovingEmployeeId = approvingEmployeeId;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            ModifiedAt = modifiedAt;
            ModifiedBy = modifiedBy;
        }
    }
}
