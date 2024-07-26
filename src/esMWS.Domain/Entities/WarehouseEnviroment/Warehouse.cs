using esMWS.Domain.Entities.Documents;

namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class Warehouse
    {
        public string WarehouseId { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public IList<Zone> Zones { get; set; } = [];
        public IList<WarehouseUnit> WarehouseUnits { get; set; } = [];
        public IList<BaseDocument> Documents { get; set; } = [];
        public IList<MMP> MMPDocuments { get; set; } = [];
        public IList<MMM> MMMDocuments { get; set; } = [];
    }
}
