using esWMS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Zones.Queries.GetSortedFilteredZones
{
    public record GetSortedFilteredZonesQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<ZoneDto>>>;
}
