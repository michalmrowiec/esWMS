﻿using AutoMapper;
using esWMS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;
using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Functions.Locations.Queries.GetSortedFilteredLocations
{
    internal class GetSortedFilteredLocationsQueryHandler
        (ILocationRepository locationRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredLocationsQuery, BaseResponse<PagedResult<LocationDto>>>
    {
        private readonly ILocationRepository _locationRepository = locationRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<LocationDto>>> Handle
            (GetSortedFilteredLocationsQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<LocationDto, Location>(_mapper, _locationRepository);
        }
    }
}