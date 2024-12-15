using esWMS.Application.Functions.Products;
using FluentValidation;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.MoveWarehouseUnitItem
{
    internal class MoveWarehouseUnitItemValidator : AbstractValidator<MoveWarehouseUnitItemCommand>
    {
        public MoveWarehouseUnitItemValidator(
            IEnumerable<WarehouseUnitItemDto> warehouseUnitItems,
            IEnumerable<ProductDto> products)
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

                    item.RuleFor(x => x)
                        .Custom((value, context) =>
                        {
                            var warehouseUnitItem = warehouseUnitItems.FirstOrDefault(x => x.WarehouseUnitItemId == value.WarehouseUnitItemId);

                            if (warehouseUnitItem == null)
                            {
                                context.AddFailure($"Something went wrong.");
                                return;
                            }

                            var product = products.FirstOrDefault(p => p.ProductId == warehouseUnitItem.ProductId);

                            if (product == null)
                            {
                                context.AddFailure($"Something went wrong.");
                                return;
                            }

                            if (!product.IsWeight && value.Quantity % 1 != 0)
                            {
                                context.AddFailure("Quantity must be a whole number for non-weight products.");
                            }
                        });
                });
        }
    }
}
