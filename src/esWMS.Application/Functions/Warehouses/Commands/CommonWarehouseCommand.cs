namespace esWMS.Application.Functions.Warehouses.Commands
{
    public abstract class CommonWarehouseCommand
    {
        public string WarehouseId { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
