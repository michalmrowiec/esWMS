using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esMWS.Domain.Models
{
    public class WarehouseStock : Product
    {
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}
