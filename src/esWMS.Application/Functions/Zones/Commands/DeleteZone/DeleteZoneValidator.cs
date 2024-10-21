using esWMS.Application.Functions.Locations;
using FluentValidation;

namespace esWMS.Application.Functions.Zones.Commands.DeleteZone
{
    internal class DeleteZoneValidator : AbstractValidator<DeleteZoneCommand>
    {
        public DeleteZoneValidator(IEnumerable<LocationDto> locations)
        {
            RuleFor(x => x.ZoneId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (locations.Any())
                    {
                        context.AddFailure(
                            "ZoneId",
                            $"Cannot delete zone because it has locations: {string.Join(',', locations.Select(x => x.LocationId))}");
                    }
                });
        }
    }
}
