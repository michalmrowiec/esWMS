using esWMS.Domain.Entities.SystemActors;

namespace esWMS.Domain.Entities.Documents
{
    public class PZ : BaseDocument
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string SupplierContractorId { get; set; } = null!;

        public Contractor? SupplierContractor { get; set; }
    }
}
