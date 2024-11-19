using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Zones.Commands.CreateZone
{
    public class CreateZoneCommand : CommonZoneCommand, IRequest<BaseResponse<ZoneDto>>
    {
        public char ZoneAlias { get; set; }
        public string WarehouseId { get; set; } = null!;
        public string? CreatedBy { get; set; }
    }
}
