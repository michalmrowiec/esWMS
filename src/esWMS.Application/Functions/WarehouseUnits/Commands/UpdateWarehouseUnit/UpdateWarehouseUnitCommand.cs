using esWMS.Application.Functions.WarehouseUnitItems.Commands.UpdateWarehouseUnitItem;
using esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.UpdateWarehouseUnit
{
    public class UpdateWarehouseUnitCommand : CreateFlatWarehouseUnitCommand
    {
        public string WarehouseUnitId { get; set; } = null!;
        public IList<UpdateWarehouseUnitItemCommand> WarehouseUnitItems { get; set; } = [];
    }
}
