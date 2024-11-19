using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Locations.Queries.GetLocationById
{
    public record class GetLocationByIdQuery(string LocationId)
        : IRequest<BaseResponse<LocationDto>>;
}
