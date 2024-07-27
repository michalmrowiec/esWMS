using esMWS.Domain.Entities.SystemActors;

namespace esMWS.Domain.Entities.Documents
{
    public class WZ : BaseDocument
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string RecipientContractorId { get; set; } = null!;

        public Contractor? RecipientContractor { get; set; }
    }
}
