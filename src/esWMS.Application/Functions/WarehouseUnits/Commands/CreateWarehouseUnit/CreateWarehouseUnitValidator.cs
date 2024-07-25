using esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem;
using FluentValidation;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit
{
    internal class CreateWarehouseUnitValidator : AbstractValidator<CreateWarehouseUnitCommand>
    {
        public CreateWarehouseUnitValidator()
        {
            RuleForEach(x => x.WarehouseUnitItems)
                .SetValidator(new CreateWarehouseUnitItemValidator(false));
        }
    }
}
