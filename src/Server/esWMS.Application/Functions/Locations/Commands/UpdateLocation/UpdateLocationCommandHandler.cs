using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Services;
using MediatR;

namespace esWMS.Application.Functions.Locations.Commands.UpdateLocation
{
    internal class UpdateLocationCommandHandler
        : IRequestHandler<UpdateLocationCommand, BaseResponse<LocationDto>>
    {
        private readonly ILocationRepository _repository;
        private readonly IMapper _mapper;

        public UpdateLocationCommandHandler
            (ILocationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<LocationDto>> Handle(
            UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateLocationValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<LocationDto>(validationResult);
            }

            try
            {
                var existingEntity = await _repository.GetByIdAsync(request.LocationId);

                var mapped = _mapper.Map(request, existingEntity);

                var updated = await _repository.UpdateAsync(mapped);

                var mappedDto = _mapper.Map<LocationDto>(updated);

                return new BaseResponse<LocationDto>(mappedDto);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse<LocationDto>
                    (BaseResponse.ResponseStatus.NotFound, "Location not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<LocationDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }
        }
    }
}
