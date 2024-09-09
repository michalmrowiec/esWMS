using esWMS.Application.Functions.Locations;
using esWMS.Application.Functions.Locations.Queries.GetLocationById;
using esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem;
using esWMS.Application.Responses;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit
{
    internal class CreateWarehouseUnitValidator : AbstractValidator<CreateWarehouseUnitCommand>
    {
        public CreateWarehouseUnitValidator(IMediator mediator)
        {
            RuleForEach(x => x.WarehouseUnitItems)
                .SetValidator(new CreateWarehouseUnitItemValidator(false));

            RuleFor(x => x.LocationId)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (string.IsNullOrWhiteSpace(value))
                        return;

                    var locationResponse = await mediator.Send(
                        new GetLocationByIdQuery(value ?? ""));

                    if (locationResponse == null)
                    {
                        context.AddFailure("Something went wrong while retrieving locations.");
                        return;
                    }

                    if (locationResponse.Status == BaseResponse.ResponseStatus.NotFound
                        || locationResponse.ReturnedObj == null)
                    {
                        context.AddFailure($"Location with ID: {value} not found.");
                        return;
                    }

                    if (!locationResponse.IsSuccess())
                    {
                        context.AddFailure($"Something went wrong while retrieving locations: {locationResponse.Message}");
                        return;
                    }

                    if (locationResponse.ReturnedObj.IsFull)
                    {
                        context.AddFailure($"Location with ID: {value} is full.");
                    }
                });
        }
    }
}
