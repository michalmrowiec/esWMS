namespace esWMS.Client.ViewModels
{
    public class WarehouseStockVM
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public int Quantity { get; set; }
        public int BlockedQuantity { get; set; }
        public decimal Value { get; set; }
    }
}
