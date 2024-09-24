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

            RuleFor(x => x.WarehouseUnitItemWithQuantity)
                .NotNull()
                .NotEmpty();

            RuleForEach(x => x.WarehouseUnitItemWithQuantity)
                .ChildRules(item =>
                {
                    item.RuleFor(x => x.WarehouseUnitItemId)
                        .NotNull()
                        .NotEmpty();

                    item.RuleFor(x => x.Quantity)
                        .GreaterThan(0);
                });
        }
    }
}
