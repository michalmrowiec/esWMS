using esMWS.Domain.Entities.WarehouseEnviroment;
using FluentValidation;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.DeleteWarehouseUnit
{
    internal class DeleteWarehouseUnitValidator : AbstractValidator<DeleteWarehouseUnitCommand>
    {
        public DeleteWarehouseUnitValidator(WarehouseUnit warehouseUnit)
        {
            RuleFor(x => x.WarehouseUnitId)
                .NotNull()
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if(warehouseUnit!.WarehouseUnitItems.Count > 0)
                    {
                        context.AddFailure("WarehouseUnitId", "Warehouse unit has items.");
                    }
                });
        }
    }
}
