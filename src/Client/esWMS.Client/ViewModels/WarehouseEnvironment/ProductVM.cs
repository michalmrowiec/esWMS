using FluentValidation;

namespace esWMS.Client.ViewModels.WarehouseEnvironment
{
    public class ProductVM
    {
        public string ProductId { get; set; } = null!;
        public string ProductCode { get; set; } = null!;
        public string? EanCode { get; set; }
        public string ProductName { get; set; } = null!;
        public string ShortProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public string CategoryId { get; set; } = null!;
        public string? Unit { get; set; }
        public bool IsWeight { get; set; }
        public double? TotalWeight { get; set; }
        public double? TotalLength { get; set; }
        public double? TotalWidth { get; set; }
        public double? TotalHeight { get; set; }
        public double? MinStorageTemperature { get; set; }
        public double? MaxStorageTemperature { get; set; }
        public bool IsMedia { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public int? VatRate { get; set; }
        public string? SupplierContractorId { get; set; }
        public bool IsActive { get; set; }

        public CategoryVM? Category { get; set; }
    }

    public class CreateProductVM
    {
        public string ProductCode { get; set; } = null!;
        public string? EanCode { get; set; }
        public string ProductName { get; set; } = null!;
        public string ShortProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public string CategoryId { get; set; } = null!;
        public string? Unit { get; set; }
        public bool IsWeight { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? TotalLength { get; set; }
        public decimal? TotalWidth { get; set; }
        public decimal? TotalHeight { get; set; }
        public decimal? MinStorageTemperature { get; set; }
        public decimal? MaxStorageTemperature { get; set; }
        public bool IsMedia { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; } = "PLN";
        public int? VatRate { get; set; }
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

            RuleFor(x => x.ShortProductName)
                .NotEmpty().WithMessage("Pole Krótka nazwa produktu jest wymagane.")
                .MaximumLength(35).WithMessage("Pole Krótka nazwa produktu może zawierać maksymalnie 35 znaków.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Pole Identyfikator kategorii jest wymagane.")
                .MaximumLength(50).WithMessage("Pole Identyfikator kategorii może zawierać maksymalnie 50 znaków.");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Pole Jednostka jest wymagane.")
                .MaximumLength(10).WithMessage("Pole Jednostka może zawierać maksymalnie 10 znaków.");

            RuleFor(x => x.IsWeight)
                .NotNull().WithMessage("Pole Czy waga jest wymagane.");

            RuleFor(x => x.IsMedia)
                .NotNull().WithMessage("Pole Czy media jest wymagane.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Pole Cena jest wymagane.")
                .GreaterThan(0).WithMessage("Cena musi być większa od 0.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Pole Czy aktywny jest wymagane.");

            RuleFor(x => x.TotalWeight)
                .GreaterThanOrEqualTo(0).WithMessage("Waga musi być większa lub równa 0.");

            RuleFor(x => x.TotalLength)
                .GreaterThanOrEqualTo(0).WithMessage("Długość musi być większa lub równa 0.");

            RuleFor(x => x.TotalWidth)
                .GreaterThanOrEqualTo(0).WithMessage("Szerokość musi być większa lub równa 0.");

            RuleFor(x => x.TotalHeight)
                .GreaterThanOrEqualTo(0).WithMessage("Wysokość musi być większa lub równa 0.");

            RuleFor(x => x.MinStorageTemperature)
                .GreaterThanOrEqualTo(-100).WithMessage("Minimalna temperatura składowania musi być większa lub równa -100.")
                .LessThanOrEqualTo(100).WithMessage("Minimalna temperatura składowania musi być mniejsza lub równa 100.");

            RuleFor(x => x.MaxStorageTemperature)
                .GreaterThanOrEqualTo(x => x.MinStorageTemperature).WithMessage("Maksymalna temperatura składowania musi być większa lub równa minimalnej temperaturze.")
                .LessThanOrEqualTo(100).WithMessage("Maksymalna temperatura składowania musi być mniejsza lub równa 100.");

            RuleFor(x => x.VatRate)
                .GreaterThanOrEqualTo(0).WithMessage("Stawka VAT musi być większa lub równa 0.")
                .LessThanOrEqualTo(100).WithMessage("Stawka VAT musi być mniejsza lub równa 100.");

            RuleFor(x => x.SupplierContractorId)
                .MaximumLength(50).WithMessage("Pole Identyfikator dostawcy może zawierać maksymalnie 50 znaków.");

            RuleFor(x => x.Currency)
                .MaximumLength(5).WithMessage("Pole Waluta może zawierać maksymalnie 5 znaków.");
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
