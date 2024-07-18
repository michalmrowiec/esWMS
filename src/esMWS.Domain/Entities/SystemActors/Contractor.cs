using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.WarehouseEnviroment;
using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.SystemActors
{
    public class Contractor
    {
        [Required]
        [MaxLength(3)]
        public string ContractorId { get; set; } = null!;
        [Required]
        [MaxLength(450)]
        public string ContractorName { get; set; } = null!;
        [MaxLength(30)]
        public string? VatId { get; set; }
        [Required]
        public bool IsSupplier { get; set; }
        [Required]
        public bool IsRecipient { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string Region { get; set; }
        [MaxLength(25)]
        public string PostalCode { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(255)]
        public string EmailAddress { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public IList<PZ>? PZDocuments { get; set; }
        public IList<WZ>? WZDocuments { get; set; }
        public IList<Product>? Products { get; set; }
    }
}
