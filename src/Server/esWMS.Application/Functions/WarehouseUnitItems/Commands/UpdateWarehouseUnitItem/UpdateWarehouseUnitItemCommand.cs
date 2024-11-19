using esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.UpdateWarehouseUnitItem
{
    public class UpdateWarehouseUnitItemCommand
        : CreateWarehouseUnitItemCommand
    {
        public string WarehouseUnitItemId { get; set; } = null!;
    }
}
