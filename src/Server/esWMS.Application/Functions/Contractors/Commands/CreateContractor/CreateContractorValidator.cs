using FluentValidation;

namespace esWMS.Application.Functions.Contractors.Commands.CreateContractor
{
    internal class CreateContractorValidator : CommonContractorValidator<CreateContractorCommand>
    {
        public CreateContractorValidator()
        {
            RuleFor(c => c.CreatedBy)
                .MaximumLength(60);
        }
    }
}
