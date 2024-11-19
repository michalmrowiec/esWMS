using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.DeleteWzItem
{
    public record DeleteWzItemCommand(string DocumentItemId) : IRequest<BaseResponse>;
}
