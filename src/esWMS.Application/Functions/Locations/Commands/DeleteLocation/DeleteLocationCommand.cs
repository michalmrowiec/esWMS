using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Locations.Commands.DeleteLocation
{
    public record DeleteLocationCommand(string LocationId) : IRequest<BaseResponse>;
}
