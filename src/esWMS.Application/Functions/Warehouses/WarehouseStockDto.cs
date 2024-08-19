using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Functions.Categories;
using esWMS.Application.Functions.Products;

namespace esWMS.Application.Functions.Warehouses
{
    public class WarehouseStockDto
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