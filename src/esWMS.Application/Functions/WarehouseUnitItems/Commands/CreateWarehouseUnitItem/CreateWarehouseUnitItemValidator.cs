using FluentValidation;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem
{
    internal class CreateWarehouseUnitItemValidator : AbstractValidator<CreateWarehouseUnitItemCommand>
    {
        public CreateWarehouseUnitItemValidator(bool warehouseUnitIdCheck)
        {
            if (warehouseUnitIdCheck)
            {
                RuleFor(x => x.WarehouseUnitId)
                    .NotNull()
                    .NotEmpty();
            }
        }
    }
}
