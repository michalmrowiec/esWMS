using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.RwFunctions.Commands.ApproveRwItems
{
    public class ApproveRwItemsCommand : IRequest<BaseResponse<RwDto>>
    {
        public string DocumentId { get; set; } = null!;
        public List<CreateDocumentWarehouseUnitItemCommand> DocumentItemsWithAssignment { get; set; } = null!;

        public string? ModifiedBy { get; set; }
    }
}
