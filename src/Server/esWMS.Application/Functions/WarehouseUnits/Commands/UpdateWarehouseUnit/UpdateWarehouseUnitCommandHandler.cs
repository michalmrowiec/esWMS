using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.UpdateWarehouseUnit
{
    internal class UpdateWarehouseUnitCommandHandler(
        IWarehouseUnitRepository repository,
        IMediator mediator,
        IMapper mapper)
        : IRequestHandler<UpdateWarehouseUnitCommand, BaseResponse<WarehouseUnitDto>>
    {
        private readonly IWarehouseUnitRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<WarehouseUnitDto>> Handle(
            UpdateWarehouseUnitCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateWarehouseUnitValidator()
                .ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WarehouseUnitDto>(validationResult);
            }

            try
            {
                var existingEntity = await _repository.GetByIdAsync(request.WarehouseUnitId);

                var mapped = _mapper.Map(request, existingEntity);

                var updated = await _repository.UpdateAsync(mapped);

                var mappedDto = _mapper.Map<WarehouseUnitDto>(updated);

                return new BaseResponse<WarehouseUnitDto>(mappedDto);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse<WarehouseUnitDto>
                    (BaseResponse.ResponseStatus.NotFound, "Warehouse unit not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<WarehouseUnitDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }
        }
    }
}
