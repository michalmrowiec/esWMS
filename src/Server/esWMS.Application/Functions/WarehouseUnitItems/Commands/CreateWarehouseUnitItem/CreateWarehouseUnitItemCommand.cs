using esWMS.Application.Responses;
using MediatR;
using System.ComponentModel;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem
{
    public class CreateWarehouseUnitItemCommand : IRequest<BaseResponse<WarehouseUnitItemDto>>
    {
        public string WarehouseUnitId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public bool IsMediaOfWarehouseUnit { get; set; } = false;
        public decimal Quantity { get; set; }
        public decimal BlockedQuantity { get; set; }
        public DateTime? BestBefore { get; set; }
        [DisplayName("Batch/Lot")]
        public string? BatchLot { get; set; }
        public string? SerialNumber { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public string? Unit { get; set; }
        public int? VatRate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
