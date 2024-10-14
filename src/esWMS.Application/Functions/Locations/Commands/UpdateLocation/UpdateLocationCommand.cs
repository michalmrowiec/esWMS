using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommand : CommonLocationCommand, IRequest<BaseResponse<LocationDto>>
    {
        public string LocationId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
