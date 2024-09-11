using esMWS.Domain.Entities.WarehouseEnviroment;
using FluentValidation;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.SetStackOnForWarehouseUnit
{
    internal class SetStackOnForWarehouseUnitValidator : AbstractValidator<SetStackOnForWarehouseUnitCommand>
    {
        public SetStackOnForWarehouseUnitValidator
            (WarehouseUnit? warehouseUnit, WarehouseUnit? stackOnWarehouseUnit)
        {
            RuleFor(x => x.WarehouseUnitId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Warehouse unit ID is required.")
                .Custom((value, context) =>
                {
                    if (warehouseUnit == null)
                    {
                        context.AddFailure($"Warehouse unit with ID: {value} not found.");
                    }

                    if (warehouseUnit!.CanBeStacked != true)
                    {
                        context.AddFailure($"Warehouse unit with ID: {value} cannot be stacked.");
                    }

                    if (warehouseUnit.WarehouseUnitId == stackOnWarehouseUnit?.StackOnId)
                    {
                        context.AddFailure($"Warehouse unit with ID: {value} cannot be stacked on a warehouse unit that is already stacked on it.");
                    }

                    if (warehouseUnit.WarehouseUnitId == stackOnWarehouseUnit?.WarehouseUnitId)
                    {
                        context.AddFailure($"Warehouse unit with ID: {value} cannot be stacked on itself.");
                    }
                });

            RuleFor(x => x.StackOnWarehouseUnitId)
                .Custom((value, context) =>
                {
                    if (string.IsNullOrWhiteSpace(value))
                        return;

                    if (stackOnWarehouseUnit == null)
                    {
                        context.AddFailure($"Warehouse unit with ID: {value} not found.");
                        return;
                    }

                    if (stackOnWarehouseUnit?.LocationId != warehouseUnit.LocationId)
                    {
                        context.AddFailure($"Warehouse unit with ID: {value} is not in the same location. Must be the same or empty.");
                    }

                    if (stackOnWarehouseUnit.CanBeStacked != true)
                    {
                        context.AddFailure($"Warehouse unit with ID: {value} cannot be stacked.");
                    }
                });
        }
    }
}
