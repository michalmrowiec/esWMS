using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Zones.Commands.CreateZone
{
    internal class CreateZoneCommandHandler
        : IRequestHandler<CreateZoneCommand, BaseResponse<ZoneDto>>
    {
        private readonly IZoneRepository _repository;
        private readonly IMapper _mapper;

        public CreateZoneCommandHandler
            (IZoneRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ZoneDto>> Handle
            (CreateZoneCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await new CreateZoneValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<ZoneDto>(validationResult);
            }

            var entity = _mapper.Map<Zone>(request);

            entity.ZoneId = $"{entity.WarehouseId}/{entity.ZoneAlias}".ToUpper();

            var createdEntity = await _repository.CreateAsync(entity);

            var entityDto = _mapper.Map<ZoneDto>(createdEntity);

            return new BaseResponse<ZoneDto>(entityDto);
        }
    }
}
