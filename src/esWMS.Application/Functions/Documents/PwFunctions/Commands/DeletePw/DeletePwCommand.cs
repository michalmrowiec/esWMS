using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.DeletePw
{
    public record DeletePwCommand(string DocumentId) : IRequest<BaseResponse>;
}
