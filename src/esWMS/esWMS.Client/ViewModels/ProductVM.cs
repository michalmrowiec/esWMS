using FluentValidation;

namespace esWMS.Client.ViewModels
{
    public class ProductVM
    {
        public string ProductId { get; set; } = null!;
        public string ProductCode { get; set; } = null!;
        public string? EanCode { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public string CategoryId { get; set; } = null!;
        public string? Unit { get; set; }
        public bool IsWeight { get; set; }
        public int? WeightPerUnit { get; set; }
        public int? StorageTemperature { get; set; }
        public bool IsMedia { get; set; }
        public string? MediaTypeAlias { get; set; }
        public decimal? Price { get; set; }
        public string? SupplierContractorId { get; set; }
        public bool IsActive { get; set; }

        public CategoryVM? Category { get; set; }
    }

    public class CreateProductVM
    {
        public string ProductCode { get; set; } = null!;
        public string? EanCode { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public string CategoryId { get; set; } = null!;
        public string? Unit { get; set; }
        public bool IsWeight { get; set; }
        public int? WeightPerUnit { get; set; }
        public int? StorageTemperature { get; set; }
        public bool IsMedia { get; set; } = false;
        public string? MediaTypeAlias { get; set; }
        public decimal? Price { get; set; }
        public string? SupplierContractorId { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CreateProductVMValidator : AbstractValidator<CreateProductVM>
    {
        public CreateProductVMValidator()
        {
            RuleFor(x => x.ProductCode)
                .NotEmpty().WithMessage("Pole Kod produktu jest wymagane.")
                .MaximumLength(100).WithMessage("Pole Kod produktu może zawierać maksymalnie 100 znaków.");

            RuleFor(x => x.EanCode)
                .MaximumLength(100).WithMessage("Pole Kod EAN może zawierać maksymalnie 100 znaków.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Pole Nazwa produktu jest wymagane.")
                .MaximumLength(250).WithMessage("Pole Nazwa produktu może zawierać maksymalnie 250 znaków.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Pole Identyfikator kategorii jest wymagane.")
                .MaximumLength(50).WithMessage("Pole Identyfikator kategorii może zawierać maksymalnie 50 znaków.");

            RuleFor(x => x.Unit)
                .MaximumLength(10).WithMessage("Pole Jednostka może zawierać maksymalnie 10 znaków.");

            RuleFor(x => x.IsWeight)
                .NotNull().WithMessage("Pole Czy waga jest wymagane.");

            RuleFor(x => x.IsMedia)
                .NotNull().WithMessage("Pole Czy media jest wymagane.");

            RuleFor(x => x.MediaTypeAlias)
                .MaximumLength(10).WithMessage("Pole Alias typu mediów może zawierać maksymalnie 10 znaków.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Pole Cena jest wymagane.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Pole Czy aktywny jest wymagane.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateProductVM>.CreateWithOptions((CreateProductVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
