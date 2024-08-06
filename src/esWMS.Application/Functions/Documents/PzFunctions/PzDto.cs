using esWMS.Application.Functions.Contractors;
using esWMS.Application.Functions.Documents.BaseDocumentFunctions;

namespace esWMS.Application.Functions.Documents.PzFunctions
{
    public class PzDto : BaseDocumentDto
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string SupplierContractorId { get; set; } = null!;

        public ContractorDto? SupplierContractor { get; set; }
    }
}
