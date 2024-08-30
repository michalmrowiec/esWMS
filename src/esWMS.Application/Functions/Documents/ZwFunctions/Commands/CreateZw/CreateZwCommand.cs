using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.CreateZw
{
    public class CreateZwCommand
        : CreateBaseDocumentCommand, IRequest<BaseResponse<ZwDto>>
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string? DepartmentName { get; set; }
    }
}
