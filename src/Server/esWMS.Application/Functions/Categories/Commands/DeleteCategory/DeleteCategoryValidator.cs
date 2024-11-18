using esWMS.Application.Functions.Products;
using FluentValidation;

namespace esWMS.Application.Functions.Categories.Commands.DeleteCategory
{
    internal class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator(IEnumerable<ProductDto> products)
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (products.Any())
                    {
                        context.AddFailure(
                            "CategoryId",
                            $"You can't delete a category that is assigned to existing products. The category is assigned to products with codes: {string.Join(", ", products.Select(x => x.ProductCode))}");
                    }
                });
        }
    }
}
