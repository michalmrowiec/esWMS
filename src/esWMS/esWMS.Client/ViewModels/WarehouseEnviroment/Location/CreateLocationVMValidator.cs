using FluentValidation;

namespace esWMS.Client.ViewModels.WarehouseEnvironment.Location
{
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
