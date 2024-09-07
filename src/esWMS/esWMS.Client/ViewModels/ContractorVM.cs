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