using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.RwFunctions.Commands.DeleteRw
{
    public record DeleteRwCommand(string DocumentId) : IRequest<BaseResponse>;
}
