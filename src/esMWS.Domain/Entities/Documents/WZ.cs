using esMWS.Domain.Entities.SystemActors;
using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.Documents
{
    public class WZ : DocumentBase
    {
        public DateTime? GoodsReleaseDate { get; set; }
        [Required]
        public string RecipientContractorId { get; set; } = null!;

        public Contractor? RecipientContractor { get; set; }
    }
}
