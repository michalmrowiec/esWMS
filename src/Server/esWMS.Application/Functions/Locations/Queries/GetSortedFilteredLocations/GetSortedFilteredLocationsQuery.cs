using esWMS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Locations.Queries.GetSortedFilteredLocations
{
    public record GetSortedFilteredLocationsQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<LocationDto>>>;
}
