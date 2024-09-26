﻿using AutoMapper;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Locations.Commands.CreateLocation
{
    internal class CreateLocationCommandHandler
        : IRequestHandler<CreateLocationCommand, BaseResponse<LocationDto>>
    {
        private readonly ILocationRepository _repository;
        private readonly IMapper _mapper;

        public CreateLocationCommandHandler
            (ILocationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<LocationDto>> Handle
            (CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await new CreateLocationValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<LocationDto>(validationResult);
            }

            LocationDto entityDto;
            try
            {
                var entity = _mapper.Map<Location>(request);

                entity.LocationId =
                    $"{entity.ZoneId}/{entity.Row:D2}/{entity.Column}/{entity.Level}/{entity.Cell}".ToUpper();

                var createdEntity = await _repository.CreateAsync(entity);

                entityDto = _mapper.Map<LocationDto>(createdEntity);
            }
            catch (Exception ex)
            {
                return new BaseResponse<LocationDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<LocationDto>(entityDto);
        }
    }
}
