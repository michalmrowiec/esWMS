using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    public class CreateDocumentItemCommand : IRequest<BaseResponse<DocumentItemDto>>
    {
        public string? DocumentId { get; set; }
        public string ProductId { get; set; } = null!;
        public string ProductCode { get; set; } = null!; // TODO auto fill on create new doc, base on prodId
        public string? EanCode { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public bool IsApproved { get; set; }
        public string? CreatedBy { get; set; }

        public List<DocumentItemWithAssignment> DocumentItemsWithAssignment { get; set; } = [];
    }
}
