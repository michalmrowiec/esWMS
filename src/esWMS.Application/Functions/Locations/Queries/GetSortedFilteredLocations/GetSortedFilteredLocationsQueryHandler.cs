using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Services;
using esWMS.Application.Responses;
using MediatR;

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