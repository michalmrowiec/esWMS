using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Locations;
using esWMS.Application.Functions.Locations.Queries.GetLocationById;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.SetLocationForWarehouseUnit
{
    internal class SetLocationForWarehouseUnitCommandHandler
        (IWarehouseUnitRepository warehouseUnitRepository,
        IMapper mapper,
        IMediator mediator)
        : IRequestHandler<SetLocationForWarehouseUnitCommand, BaseResponse<WarehouseUnitDto>>
    {
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<WarehouseUnitDto>> Handle
            (SetLocationForWarehouseUnitCommand request, CancellationToken cancellationToken)
        {
            WarehouseUnit? warehouseUnit;
            try
            {
                warehouseUnit = await _warehouseUnitRepository.GetByIdAsync(request.WarehouseUnitId);

            }
            catch (Exception ex)
            {
                return new BaseResponse<WarehouseUnitDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong");
            }

            BaseResponse<LocationDto>? newLocationResponse = null;
            if (!string.IsNullOrWhiteSpace(request.NewLocationId))
            {
                newLocationResponse = await _mediator.Send(
                    new GetLocationByIdQuery(request.NewLocationId));
            }

            var validationResult = await new SetLocationForWarehouseUnitValidator
                (warehouseUnit, newLocationResponse).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WarehouseUnitDto>(validationResult);
            }

            if (!string.IsNullOrWhiteSpace(request.NewLocationId))
            {
                warehouseUnit.LocationId = newLocationResponse!.ReturnedObj!.LocationId;
            }
            else
            {
                warehouseUnit.LocationId = null;
            }
            warehouseUnit.ModifiedAt = DateTime.Now;
            warehouseUnit.ModifiedBy = request.ModifiedBy;

            WarehouseUnitDto updatedWarehouseUnitDto;

            try
            {
                var updatedWarehouseUnit = await _warehouseUnitRepository.UpdateAsync(warehouseUnit);

                updatedWarehouseUnitDto = _mapper.Map<WarehouseUnitDto>(updatedWarehouseUnit);
            }
            catch (Exception ex)
            {
                return new BaseResponse<WarehouseUnitDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong");
            }

            return new BaseResponse<WarehouseUnitDto>(updatedWarehouseUnitDto);
        }
    }
}
