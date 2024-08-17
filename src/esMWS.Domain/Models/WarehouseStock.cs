using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esMWS.Domain.Models
{
    public class WarehouseStock
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Value { get; set; }

        public Product? Product { get; set; }
        public Category? Category { get; set; }
    }
}
