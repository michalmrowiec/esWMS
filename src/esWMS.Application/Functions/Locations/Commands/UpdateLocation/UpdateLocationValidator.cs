using FluentValidation;

namespace esWMS.Application.Functions.Locations.Commands.UpdateLocation
{
    internal class UpdateLocationValidator : CommonLocationValidator<UpdateLocationCommand>
    {
        public UpdateLocationValidator()
        {
            RuleFor(x => x.ModifiedBy)
                .MaximumLength(60);
        }
    }
}
