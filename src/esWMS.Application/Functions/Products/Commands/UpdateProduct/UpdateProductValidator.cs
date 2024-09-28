using esWMS.Application.Functions.Products.Commands.UpdateProduct;
using FluentValidation;

namespace esWMS.Application.Functions.Products.Commands.CreateProduct
{
    internal class UpdateProductValidator : CommonProductValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ModifiedBy)
                .MaximumLength(60);
        }
    }
}
