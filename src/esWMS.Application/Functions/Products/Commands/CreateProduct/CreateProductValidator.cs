using FluentValidation;

namespace esWMS.Application.Functions.Products.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductCode)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.EanCode)
                .MaximumLength(100);

            RuleFor(x => x.ProductName)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Unit)
                .MaximumLength(10);

            RuleFor(x => x.IsWeight)
                .NotNull();

            RuleFor(x => x.IsMedia)
                .NotNull();

            //RuleFor(x => x.MediaTypeAlias)
            //    .MaximumLength(10);

            RuleFor(x => x.Price)
                .NotNull();

            RuleFor(x => x.IsActive)
                .NotNull();

            RuleFor(x => x.CreatedBy)
                .MaximumLength(60);
        }
    }
}
