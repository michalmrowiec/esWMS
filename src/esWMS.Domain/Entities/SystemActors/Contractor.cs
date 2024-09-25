using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Domain.Entities.SystemActors
{
    public class Contractor
    {
        public string ContractorId { get; set; } = null!;
        public string ContractorName { get; set; } = null!;
        public string? VatId { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsRecipient { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public IList<PZ> PZDocuments { get; set; } = [];
        public IList<WZ> WZDocuments { get; set; } = [];
        public IList<Product> Products { get; set; } = [];
    }
}
