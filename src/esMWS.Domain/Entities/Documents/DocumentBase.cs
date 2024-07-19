using esMWS.Domain.Entities.SystemActors;
using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esMWS.Domain.Entities.Documents
{
    public abstract class DocumentBase
    {
        public string DocumentId { get; set; } = null!;
        public string IssueWarehouseId { get; set; } = null!;
        public string? Comment { get; set; }
        public DateTime DocumentIssueDate { get; set; }
        public string? IssuingEmployeeId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? AprovedDate { get; set; }
        public string? ApprovingEmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public Warehouse? IssueWarehouse { get; set; }
        public Employee? IssuingEmployee { get; set; }
        public Employee? ApprovingEmployee { get; set; }
        public IList<DocumentItem>? DocumentItems { get; set; }
    }
}
