namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions
{
    public class DocumentWarehouseUnitItemDto
    {
        public string DocumentItemId { get; set; } = null!;
        public string WarehouseUnitItemId { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}