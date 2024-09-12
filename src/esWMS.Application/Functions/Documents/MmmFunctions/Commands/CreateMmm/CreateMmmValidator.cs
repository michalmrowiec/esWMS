using esMWS.Domain.Entities.WarehouseEnviroment;
using FluentValidation;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.CreateMmm
{
    internal class CreateMmmValidator : AbstractValidator<CreateMmmCommand>
    {
        public CreateMmmValidator(List<WarehouseUnit> warehouseUnitsToMove)
        {
            RuleFor(x => x.WarehouseUnitIds)
                .NotEmpty()
                .NotNull()
                .Custom((value, context) =>
                {
                    if (warehouseUnitsToMove.Any(wu => wu.IsBlocked))
                    {
                        context.AddFailure(
                            "WarehouseUnitIds",
                            $"The following warehouse units are blocked: {string.Join(", ", warehouseUnitsToMove.Where(wu => wu.IsBlocked))}.");
                    }

                    foreach (var wu in warehouseUnitsToMove)
                    {
                        if (wu.WarehouseUnitItems.Count == 0)
                        {
                            context.AddFailure(
                                "WarehouseUnitIds",
                                $"Warehouse unit with ID: {wu.WarehouseUnitId} does not contain any items.");
                        }
                    }

                    var wuis = warehouseUnitsToMove.SelectMany(wu => wu.WarehouseUnitItems).ToList();

                    foreach (var wui in wuis)
                    {
                        if (wui.Quantity <= 0)
                        {

                        }

                        if (wui.BlockedQuantity != 0)
                        {
                            context.AddFailure(
                                "WarehouseUnitIds",
                                $"Warehouse unit items can only contain items that are not locked. " +
                                $"However, the warehouse unit with ID: {wui.WarehouseUnitId} has item with ID: {wui.WarehouseUnitItemId} thath has a lock on {wui.BlockedQuantity} items.");
                        }
                    }
                });

            RuleFor(x => x.ToWarehouseId)
                .NotEqual(x => x.IssueWarehouseId)
                .WithMessage("The destination warehouse cannot be the source warehouse.")
                .NotEmpty()
                .NotNull();
        }
    }
}
