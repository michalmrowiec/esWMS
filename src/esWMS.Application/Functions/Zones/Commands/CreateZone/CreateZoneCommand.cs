using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Zones.Commands.CreateZone
{
    public class CreateZoneCommand : IRequest<BaseResponse<ZoneDto>>
    {
        public string? ZoneName { get; set; }
        public char ZoneAlias { get; set; }
        public string WarehouseId { get; set; } = null!;
        public decimal? AvgTemperature { get; set; }
        public string? CreatedBy { get; set; }
    }
}
