namespace esWMS.Client.ViewModels
{
    public class WarehouseVM
    {
        public string WarehouseId { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }


        public IList<ZoneVM> Zones { get; set; } = [];
    }
}
