using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.DeleteWz
{
    public record DeleteWzCommand(string DocumentId) : IRequest<BaseResponse>;
}
