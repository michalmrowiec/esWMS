using FluentValidation;

namespace esWMS.Application.Functions.Products.Commands.CreateProduct
{
    internal class CreateProductValidator : CommonProductValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.CreatedBy)
                .MaximumLength(60);
        }
    }
}
