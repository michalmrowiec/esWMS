using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Functions.Locations;
using esWMS.Application.Responses;
using FluentValidation;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.SetLocationForWarehouseUnit
{
    internal class SetLocationForWarehouseUnitValidator : AbstractValidator<SetLocationForWarehouseUnitCommand>
    {
        public SetLocationForWarehouseUnitValidator
            (BaseResponse<IEnumerable<WarehouseUnitDto>>? warehouseUnitResponse, BaseResponse<LocationDto>? newLocationResponse)
        {
            RuleFor(x => x.WarehouseUnitId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Warehouse unit ID is required.")
                .Custom((value, context) =>
                {
                    if (warehouseUnitResponse == null)
                    {
                        context.AddFailure("Something went wrong while retrieving warehouse units.");
                        return;
                    }

                    if (warehouseUnitResponse.Status == BaseResponse.ResponseStatus.NotFound
                        || warehouseUnitResponse.ReturnedObj?.FirstOrDefault() == null)
                    {
                        context.AddFailure($"Warehouse unit with ID: {value} not found.");
                        return;
                    }

                    if (!warehouseUnitResponse.IsSuccess())
                    {
                        context.AddFailure($"Something went wrong while retrieving warehouse unit: {warehouseUnitResponse.Message}");
                        return;
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

                    if (newLocationResponse.ReturnedObj.IsBusy)
                    {
                        context.AddFailure($"Location with ID: {value} is full.");
                    }
                });
        }
    }

}
