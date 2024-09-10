using FluentValidation;

namespace esWMS.Client.ViewModels
{
    public class ContractorVM
    {
        public string ContractorId { get; set; } = null!;
        public string ContractorName { get; set; } = null!;
        public string? VatId { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsRecipient { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateContractorVM
    {
        public string? ContractorId { get; set; }
        public string? ContractorName { get; set; }
        public string? VatId { get; set; }
        public bool IsSupplier { get; set; } = false;
        public bool IsRecipient { get; set; } = false;
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CreateContractorVMValidator : AbstractValidator<CreateContractorVM>
    {
        public CreateContractorVMValidator()
        {
            RuleFor(c => c.ContractorId)
                .NotEmpty().WithMessage("Wymagane jest podanie Id kontrahenta.")
                .MaximumLength(3).WithMessage("Id kontrahenta nie może przekraczać 3 znaków.");

            RuleFor(c => c.ContractorName)
                .NotEmpty().WithMessage("Wymagane jest podanie nazwy kontrahenta.")
                .MaximumLength(50).WithMessage("Nazwa kontrahenta nie może przekraczać 50 znaków.");

            RuleFor(c => c.VatId)
                .MaximumLength(30).WithMessage("VatId nie może przekraczać 30 znaków.");

            RuleFor(c => c.IsSupplier)
                .NotNull().WithMessage("Wymagane jest określenie, czy kontrahent jest dostawcą.");

            RuleFor(c => c.IsRecipient)
                .NotNull().WithMessage("Wymagane jest określenie, czy kontrahent jest odbiorcą.");

            RuleFor(c => c.Country)
                .MaximumLength(100).WithMessage("Kraj nie może przekraczać 100 znaków.");

            RuleFor(c => c.City)
                .MaximumLength(100).WithMessage("Miasto nie może przekraczać 100 znaków.");

            RuleFor(c => c.Region)
                .MaximumLength(100).WithMessage("Region nie może przekraczać 100 znaków.");

            RuleFor(c => c.PostalCode)
                .MaximumLength(25).WithMessage("Kod pocztowy nie może przekraczać 25 znaków.");

            RuleFor(c => c.Address)
                .MaximumLength(250).WithMessage("Adres nie może przekraczać 250 znaków.");

            RuleFor(c => c.EmailAddress)
                .MaximumLength(255).WithMessage("Adres e-mail nie może przekraczać 255 znaków.");

            RuleFor(c => c.PhoneNumber)
                .MaximumLength(20).WithMessage("Numer telefonu nie może przekraczać 20 znaków.");

            RuleFor(c => c.IsActive)
                .NotNull().WithMessage("Wymagane jest określenie, czy kontrahent jest aktywny.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateContractorVM>.CreateWithOptions((CreateContractorVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}