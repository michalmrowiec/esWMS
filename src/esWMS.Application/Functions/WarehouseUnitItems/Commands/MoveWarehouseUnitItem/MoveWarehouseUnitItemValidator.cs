using FluentValidation;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.MoveWarehouseUnitItem
{
    internal class MoveWarehouseUnitItemValidator : AbstractValidator<MoveWarehouseUnitItemCommand>
    {
        public MoveWarehouseUnitItemValidator()
        {
            RuleFor(x => x.NewWarehouseUnitId)
                .NotNull()
                .NotEmpty();
        }
    }
}
