using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.SetStackOnForWarehouseUnit
{
    internal class SetStackOnForWarehouseUnitCommandHandler
        (IWarehouseUnitRepository warehouseUnitRepository,
        IMapper mapper,
        IMediator mediator)
        : IRequestHandler<SetStackOnForWarehouseUnitCommand, BaseResponse<WarehouseUnitDto>>
    {
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<WarehouseUnitDto>> Handle(SetStackOnForWarehouseUnitCommand request, CancellationToken cancellationToken)
        {
            WarehouseUnit? warehouseUnit;
            WarehouseUnit? stackOnWarehouseUnit = null;
            try
            {
                warehouseUnit = await _warehouseUnitRepository.GetByIdAsync(request.WarehouseUnitId);

                if (!string.IsNullOrWhiteSpace(request.StackOnWarehouseUnitId))
                {
                    stackOnWarehouseUnit = await _warehouseUnitRepository.GetByIdAsync(request.StackOnWarehouseUnitId);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<WarehouseUnitDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong");
            }

            var validationResult = await new SetStackOnForWarehouseUnitValidator
                (warehouseUnit, stackOnWarehouseUnit).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WarehouseUnitDto>(validationResult);
            }

            if (!string.IsNullOrWhiteSpace(request.StackOnWarehouseUnitId))
            {
                warehouseUnit.StackOnId = stackOnWarehouseUnit!.WarehouseUnitId;
                warehouseUnit.LocationId = stackOnWarehouseUnit.LocationId;
            }
            else
            {
                warehouseUnit.StackOn = null;
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
