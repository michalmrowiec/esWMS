using esWMS.Application.Functions.WarehouseUnits;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.MoveWarehouseUnitItem
{
    public class MoveWarehouseUnitItemCommand : IRequest<BaseResponse<WarehouseUnitDto>>
    {
        public IEnumerable<WarehouseUnitItemWithQuantity> WarehouseUnitItemWithQuantity { get; set; } = null!;

        public string NewWarehouseUnitId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }

    public class WarehouseUnitItemWithQuantity
    {
        public string WarehouseUnitItemId { get; set; } = null!;
        public decimal Quantity { get; set; }
    }
}
