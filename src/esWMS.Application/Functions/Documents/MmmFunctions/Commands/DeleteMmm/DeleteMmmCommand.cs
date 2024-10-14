using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.DeleteMmm
{
    public record DeleteMmmCommand(string DocumentId) : IRequest<BaseResponse>;
}
