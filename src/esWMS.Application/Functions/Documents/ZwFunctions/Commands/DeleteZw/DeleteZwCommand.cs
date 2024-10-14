using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.DeleteZw
{
    public record DeleteZwCommand(string DocumentId) : IRequest<BaseResponse>;
}
