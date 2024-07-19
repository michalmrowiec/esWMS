using esMWS.Domain.Entities.Documents;
using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class Warehouse
    {
        [Required]
        [MaxLength(3)]
        public string? WarehouseId { get; set; }
        [Required]
        [MaxLength(250)]
        public string? WarehouseName { get; set; }
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

        public IList<Zone>? Zones { get; set; }
        public IList<WarehouseUnit>? WarehouseUnits { get; set; }
        public IList<DocumentBase>? Documents { get; set; }
        public IList<MMP>? MMPDocuments { get; set; }
        public IList<MMM>? MMMDocuments { get; set; }
    }
}
