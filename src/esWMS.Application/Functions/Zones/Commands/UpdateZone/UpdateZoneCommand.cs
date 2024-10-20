using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Zones.Commands.UpdateZone
{
    public class UpdateZoneCommand : CommonZoneCommand, IRequest<BaseResponse<ZoneDto>>
    {
        public string ZoneId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
