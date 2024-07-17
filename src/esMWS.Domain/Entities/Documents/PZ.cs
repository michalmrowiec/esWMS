using esMWS.Domain.Entities.SystemActors;

namespace esMWS.Domain.Entities.Documents
{
    public class PZ : DocumentBase
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string SupplierContractorId { get; set; } = null!;

        public Contractor? SupplierContractor { get; set; }
    }
}
