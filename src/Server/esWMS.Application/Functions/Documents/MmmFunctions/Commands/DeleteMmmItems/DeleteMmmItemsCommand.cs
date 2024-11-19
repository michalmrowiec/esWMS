using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.DeleteMmmItems
{
    public record DeleteMmmItemsCommand(string DocumentId, string WarehouseUnitId) : IRequest<BaseResponse>;
}
