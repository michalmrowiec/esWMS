using esWMS.Application.Functions.Contractors;
using esWMS.Application.Functions.Documents.BaseDocumentFunctions;

namespace esWMS.Application.Functions.Documents.WzFunctions
{
    public class WzDto : BaseDocumentDto
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string RecipientContractorId { get; set; } = null!;

        public ContractorDto? RecipientContractor { get; set; }
    }
}
