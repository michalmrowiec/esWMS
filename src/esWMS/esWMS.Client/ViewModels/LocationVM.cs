using FluentValidation;

namespace esWMS.Client.ViewModels
{
    public class LocationVM
    {
        public string LocationId { get; set; } = null!;
        public string ZoneId { get; set; } = null!;
        public int Row { get; set; }
        public char Column { get; set; }
        public int Level { get; set; }
        public int Cell { get; set; }
        public decimal Capacity { get; set; }
        public decimal? MaxLength { get; set; }
        public decimal? MaxWidth { get; set; }
        public decimal? MaxHeight { get; set; }
        public decimal? MaxWeight { get; set; }
        public bool IsFull { get; set; }
        public string? DefaultMediaTypeId { get; set; }

        public ZoneVM? Zone { get; set; }
    }

    public class CreateLocationVM
    {
        public string ZoneId { get; set; }
        public int Row { get; set; } = 1;
        public char? Column { get; set; } = 'A';
        public int Level { get; set; } = 1;
        public int Cell { get; set; } = 1;
        public decimal Capacity { get; set; } = 1;
        public decimal? MaxLength { get; set; }
        public decimal? MaxWidth { get; set; }
        public decimal? MaxHeight { get; set; }
        public decimal? MaxWeight { get; set; }
        public string? DefaultMediaTypeId { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class CreateLocationVMValidator : AbstractValidator<CreateLocationVM>
    {
        public CreateLocationVMValidator()
        {
            RuleFor(l => l.ZoneId)
                .NotEmpty().WithMessage("Pole ZoneId jest wymagane.")
                .MaximumLength(5).WithMessage("Pole ZoneId może mieć maksymalnie 5 znaków.");

            RuleFor(l => l.Row)
                .NotEmpty().WithMessage("Pole Row jest wymagane.");

            RuleFor(l => l.Column)
                .NotEmpty().WithMessage("Pole Column jest wymagane.")
                .Must(c => c.HasValue && c >= 'A' && c <= 'Z').WithMessage("Pole Column może zawierać tylko jedną wielką literę od A do Z.");

            RuleFor(l => l.Level)
                .NotEmpty().WithMessage("Pole Level jest wymagane.");

            RuleFor(l => l.Cell)
                .NotEmpty().WithMessage("Pole Cell jest wymagane.");

            RuleFor(l => l.Capacity)
                .NotEmpty().WithMessage("Pole Capacity jest wymagane.")
                .GreaterThan(0).WithMessage("Pole Capacity musi być większe od 0.");
        }


        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateLocationVM>.CreateWithOptions((CreateLocationVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
