using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.DeletePz
{
    public record DeletePzCommand(string DocumentId) : IRequest<BaseResponse>;
}
