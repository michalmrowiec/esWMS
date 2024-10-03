using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.DeletePwItem
{
    public record DeletePwItemCommand(string DocumentItemId) : IRequest<BaseResponse>;
}
