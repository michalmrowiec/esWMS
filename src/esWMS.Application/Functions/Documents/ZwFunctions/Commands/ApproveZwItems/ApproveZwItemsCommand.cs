using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.ApproveZwItems
{
    public class ApproveZwItemsCommand : IRequest<BaseResponse<ZwDto>>
    {
        public string DocumentId { get; set; } = null!;
        public List<CreateDocumentWarehouseUnitItemCommand> DocumentItemsWithAssignment { get; set; } = null!;

        public string? ModifiedBy { get; set; }
    }

}
