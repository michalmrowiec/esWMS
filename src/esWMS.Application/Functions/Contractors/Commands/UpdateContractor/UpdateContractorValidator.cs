using FluentValidation;

namespace esWMS.Application.Functions.Contractors.Commands.UpdateContractor
{
    internal class UpdateContractorValidator : CommonContractorValidator<UpdateContractorCommand>
    {
        public UpdateContractorValidator()
        {
            RuleFor(c => c.ModifiedBy)
                .MaximumLength(60);
        }
    }
}
