using esWMS.Application.Functions.WarehouseUnitItems;
using FluentValidation;

namespace esWMS.Application.Functions.Products.Commands.DeleteProduct
{
    internal class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator(IEnumerable<WarehouseUnitItemDto> warehouseUnitItems)
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (warehouseUnitItems.Any())
                    {
                        context.AddFailure(
                            "ProductId",
                            $"You cannot delete a product that is currently in stock. The product is in the warehouse unit items with IDs: {string.Join(", ", warehouseUnitItems.Select(x => x.WarehouseUnitItemId))}");
                    }
                });
        }
    }
}
