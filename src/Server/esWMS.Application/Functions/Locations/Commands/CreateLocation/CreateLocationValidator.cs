using FluentValidation;

namespace esWMS.Application.Functions.Locations.Commands.CreateLocation
{
    internal class CreateLocationValidator : CommonLocationValidator<CreateLocationCommand>
    {
        public CreateLocationValidator()
        {
            RuleFor(x => x.ZoneId)
               .NotEmpty()
               .MaximumLength(5);

            RuleFor(x => x.Row)
                .NotEmpty();

            RuleFor(x => x.Column)
                .NotEmpty()
                .Must(c => c.ToString().Length == 1);

            RuleFor(x => x.Level)
                .NotEmpty();

            RuleFor(x => x.Cell)
                .NotEmpty();

            RuleFor(x => x.CreatedBy)
                .MaximumLength(60);
        }
    }
}
