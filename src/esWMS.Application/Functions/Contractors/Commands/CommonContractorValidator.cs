using FluentValidation;

namespace esWMS.Application.Functions.Contractors.Commands
{
    internal class CommonContractorValidator<T> : AbstractValidator<T>
        where T : CommonContractorCommand
    {
        public CommonContractorValidator()
        {
            RuleFor(c => c.ContractorId) // check in db exist
                .NotEmpty()
                .MaximumLength(3);

            RuleFor(c => c.ContractorName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(c => c.VatId)
                .MaximumLength(30);

            RuleFor(c => c.IsSupplier)
                .NotNull();

            RuleFor(c => c.IsRecipient)
                .NotNull();

            RuleFor(c => c.Country)
                .MaximumLength(100);

            RuleFor(c => c.City)
                .MaximumLength(100);

            RuleFor(c => c.Region)
                .MaximumLength(100);

            RuleFor(c => c.PostalCode)
                .MaximumLength(25);

            RuleFor(c => c.Address)
                .MaximumLength(250);

            RuleFor(c => c.EmailAddress)
                .MaximumLength(255);

            RuleFor(c => c.PhoneNumber)
                .MaximumLength(20);

            RuleFor(c => c.IsActive)
                .NotNull();
        }
    }
}
