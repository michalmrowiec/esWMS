namespace esWMS.Client.ViewModels
{
    public class WarehouseStockVM
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal BlockedQuantity { get; set; }
        public decimal Value { get; set; }
    }
}
