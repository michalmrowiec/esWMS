using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Zones.Commands.DeleteZone
{
    public record DeleteZoneCommand(string ZoneId) : IRequest<BaseResponse>;
}
