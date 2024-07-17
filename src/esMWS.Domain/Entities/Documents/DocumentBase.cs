using esMWS.Domain.Entities.SystemActors;
using esMWS.Domain.Entities.WarehouseEnviroment;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esMWS.Domain.Entities.Documents
{
    public abstract class DocumentBase
    {
        [Required]
        [MaxLength(20)]
        public string DocumentId { get; set; } = null!;
        [Required]
        public string IssueWarehouseId { get; set; } = null!;
        public string? Comment { get; set; }
        [Required]
        public DateTime DocumentIssueDate { get; set; }
        public string? IssuingEmployeeId { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        public DateTime? AprovedDate { get; set; }
        public string? ApprovingEmployeeId { get; set; }


        public Warehouse? IssueWarehouse { get; set; }
        public Employee? IssuingEmployee { get; set; }
        public Employee? ApprovingEmployee { get; set; }
    }
}
