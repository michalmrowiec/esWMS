﻿using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Services;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Zones.Queries.GetSortedFilteredZones
{
    internal class GetSortedFilteredZonesQueryHandler
        (IZoneRepository zoneRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredZonesQuery, BaseResponse<PagedResult<ZoneDto>>>
    {
        private readonly IZoneRepository _zoneRepository = zoneRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<ZoneDto>>> Handle
            (GetSortedFilteredZonesQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<ZoneDto, Zone>(_mapper, _zoneRepository);
        }
    }
}