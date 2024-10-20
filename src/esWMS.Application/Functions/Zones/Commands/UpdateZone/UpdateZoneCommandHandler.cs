using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Zones.Commands.UpdateZone
{
    internal class UpdateZoneCommandHandler(
        IZoneRepository repository,
        IMapper mapper)
        : IRequestHandler<UpdateZoneCommand, BaseResponse<ZoneDto>>
    {
        private readonly IZoneRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<ZoneDto>> Handle(UpdateZoneCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateZoneValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<ZoneDto>(validationResult);
            }

            try
            {
                var existingEntity = await _repository.GetByIdAsync(request.ZoneId);

                var mapped = _mapper.Map(request, existingEntity);

                var updated = await _repository.UpdateAsync(mapped);

                var mappedDto = _mapper.Map<ZoneDto>(updated);

                return new BaseResponse<ZoneDto>(mappedDto);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse<ZoneDto>
                    (BaseResponse.ResponseStatus.NotFound, "Zone not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<ZoneDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }
        }
    }
}