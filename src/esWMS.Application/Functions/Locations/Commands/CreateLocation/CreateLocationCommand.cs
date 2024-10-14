using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Locations.Commands.CreateLocation
{
    public class CreateLocationCommand : CommonLocationCommand, IRequest<BaseResponse<LocationDto>>
    {
        public string ZoneId { get; set; } = null!;
        public int Row { get; set; }
        public char Column { get; set; }
        public int Level { get; set; }
        public int Cell { get; set; }
        public string? CreatedBy { get; set; }
    }
}
