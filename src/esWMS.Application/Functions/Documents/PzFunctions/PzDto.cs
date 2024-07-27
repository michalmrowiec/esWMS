using esMWS.Domain.Entities.SystemActors;
using esWMS.Application.Functions.Documents.BaseDocumentFunctions;

namespace esWMS.Application.Functions.Documents.PzFunctions
{
    public class PzDto : BaseDocumentDto
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string SupplierContractorId { get; set; } = null!;

        public Contractor? SupplierContractor { get; set; }
    }
}
