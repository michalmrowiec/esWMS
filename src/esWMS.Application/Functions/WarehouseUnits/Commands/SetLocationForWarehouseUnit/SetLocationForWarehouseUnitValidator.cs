using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Functions.Locations;
using esWMS.Application.Responses;
using FluentValidation;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.SetLocationForWarehouseUnit
{
    internal class SetLocationForWarehouseUnitValidator : AbstractValidator<SetLocationForWarehouseUnitCommand>
    {
        public SetLocationForWarehouseUnitValidator
            (WarehouseUnit? warehouseUnit, BaseResponse<LocationDto>? newLocationResponse)
        {
            RuleFor(x => x.WarehouseUnitId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Warehouse unit ID is required.")
                .Custom((value, context) =>
                {
                    if (warehouseUnit == null)
                    {
                        context.AddFailure($"Warehouse unit with ID: {value} not found.");
                    }
                });

            RuleFor(x => x.NewLocationId)
                .NotNull()
                .NotEmpty()
                .WithMessage("New location ID is required.")
                .Custom((value, context) =>
                {
                    if (newLocationResponse == null)
                    {
                        context.AddFailure("Something went wrong while retrieving locations.");
                        return;
                    }

                    if (newLocationResponse.Status == BaseResponse.ResponseStatus.NotFound
                        || newLocationResponse.ReturnedObj == null)
                    {
                        context.AddFailure($"Location with ID: {value} not found.");
                        return;
                    }

                    if (!newLocationResponse.IsSuccess())
                    {
                        context.AddFailure($"Something went wrong while retrieving locations: {newLocationResponse.Message}");
                        return;
                    }

                    if (newLocationResponse.ReturnedObj.IsFull)
                    {
                        context.AddFailure($"Location with ID: {value} is full.");
                    }
                });
        }
    }

}
