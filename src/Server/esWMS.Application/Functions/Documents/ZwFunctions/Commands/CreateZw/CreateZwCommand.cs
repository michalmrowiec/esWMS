using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.CreateZw
{
    public class CreateZwCommand
        : CreateFlatBaseDocumentCommand, IRequest<BaseResponse<ZwDto>>
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string? DepartmentName { get; set; }

        public IList<DocumentItemIdQuantity> DocumentItemIdQuantity { get; set; } = [];
    }

    public class DocumentItemIdQuantity
    {
        public string DocumentItemId { get; set; } = null!;
        public decimal Quantity { get; set; }
    }
}
