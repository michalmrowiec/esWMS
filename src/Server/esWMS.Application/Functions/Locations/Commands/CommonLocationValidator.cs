using FluentValidation;

namespace esWMS.Application.Functions.Locations.Commands
{
    internal abstract class CommonLocationValidator<T> : AbstractValidator<T>
        where T : CommonLocationCommand
    {
        protected CommonLocationValidator()
        {
           RuleFor(x => x.Capacity)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.MaxLength)
                .GreaterThan(0).When(x => x.MaxLength.HasValue);

            RuleFor(x => x.MaxWidth)
                .GreaterThan(0).When(x => x.MaxWidth.HasValue);

            RuleFor(x => x.MaxHeight)
                .GreaterThan(0).When(x => x.MaxHeight.HasValue);

            RuleFor(x => x.MaxWeight)
                .GreaterThan(0).When(x => x.MaxWeight.HasValue);

            // add check if media is correct product
        }
    }
}
