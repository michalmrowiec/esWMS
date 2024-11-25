using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Domain.Models
{
    public class WarehouseStock
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public int Quantity { get; set; }
        public int BlockedQuantity { get; set; }
        public decimal Value { get; set; }

        public Product? Product { get; set; }
        public Category? Category { get; set; }
    }
}
