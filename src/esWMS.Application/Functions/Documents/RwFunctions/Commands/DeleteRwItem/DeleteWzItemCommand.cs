using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.RwFunctions.Commands.DeleteRwItem
{
    public record DeleteRwItemCommand(string DocumentItemId) : IRequest<BaseResponse>;
}
