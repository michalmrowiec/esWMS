using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esMWS.Domain.Entities.Documents
{
    public class MMM : BaseDocument
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string ToWarehouseId { get; set; } = null!;

        public Warehouse? ToWarehouse { get; set; }
    }
}
