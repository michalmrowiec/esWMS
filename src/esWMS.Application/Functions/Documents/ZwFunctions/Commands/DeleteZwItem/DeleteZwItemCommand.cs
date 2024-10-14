using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.DeleteZwItem
{
    public record DeleteZwItemCommand(string DocumentItemId) : IRequest<BaseResponse>;
}
