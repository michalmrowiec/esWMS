using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePzItems
{
    public class ApprovePzItemsCommand : IRequest<BaseResponse<PzDto>>
    {
        public string DocumentId { get; set; } = null!;
        public List<DocumentItemWithAssignment> DocumentItemsWithAssignment { get; set; } = null!;

        public string? ModifiedBy { get; set; }
    }

}
