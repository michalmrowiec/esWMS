using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.DeletePzItem
{
    public record DeletePzItemCommand(string DocumentItemId) : IRequest<BaseResponse>;
}
