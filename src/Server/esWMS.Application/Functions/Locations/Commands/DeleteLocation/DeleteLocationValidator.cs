using esWMS.Domain.Entities.WarehouseEnvironment;
using FluentValidation;

namespace esWMS.Application.Functions.Locations.Commands.DeleteLocation
{
    internal class DeleteLocationValidator : AbstractValidator<DeleteLocationCommand>
    {
        public DeleteLocationValidator(Location location)
        {
            RuleFor(x => x.LocationId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (location.WarehouseUnits.Any()
                        || location.IsFull)
                    {
                        context.AddFailure(
                            "LocationId",
                            $"Cannot delete the location because warehouse units are assigned to it: {string.Join(',', location.WarehouseUnits.Select(x => x.WarehouseUnitId))}");
                    }
                });
        }
    }
}
