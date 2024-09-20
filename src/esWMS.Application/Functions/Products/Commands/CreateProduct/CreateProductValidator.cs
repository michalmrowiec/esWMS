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

            RuleFor(x => x.ShortProductName)
                .NotEmpty()
                .MaximumLength(35);

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Unit)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.IsWeight)
                .NotNull();

            RuleFor(x => x.IsMedia)
                .NotNull();

            RuleFor(x => x.Price)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.IsActive)
                .NotNull();

            RuleFor(x => x.CreatedBy)
                .MaximumLength(60);

            RuleFor(x => x.TotalWeight)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.TotalLength)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.TotalWidth)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.TotalHeight)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.MinStorageTemperature)
                .GreaterThanOrEqualTo(-100)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.MaxStorageTemperature)
                .GreaterThanOrEqualTo(x => x.MinStorageTemperature)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.VatRate)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.SupplierContractorId)
                .MaximumLength(50);

            RuleFor(x => x.Currency)
                .MaximumLength(5);
        }
    }
}
