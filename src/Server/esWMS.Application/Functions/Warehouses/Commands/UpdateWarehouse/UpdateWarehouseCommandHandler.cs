using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Warehouses.Commands.UpdateWarehouse
{
    internal class UpdateWarehouseCommandHandler(
        IWarehouseRepository repository,
        IMapper mapper)
        : IRequestHandler<UpdateWarehouseCommand, BaseResponse<WarehouseDto>>
    {
        private readonly IWarehouseRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<WarehouseDto>> Handle(
            UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateWarehouseValidator()
                .ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WarehouseDto>(validationResult);
            }

            try
            {
                var existingEntity = await _repository.GetByIdAsync(request.WarehouseId);

                var mapped = _mapper.Map(request, existingEntity);

                var updated = await _repository.UpdateAsync(mapped);

                var mappedDto = _mapper.Map<WarehouseDto>(updated);

                return new BaseResponse<WarehouseDto>(mappedDto);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse<WarehouseDto>
                    (BaseResponse.ResponseStatus.NotFound, "Warehouse not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<WarehouseDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }
        }
    }
}