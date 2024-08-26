using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.CreatePw
{
    public class CreatePwCommand
        : CreateBaseDocumentCommand, IRequest<BaseResponse<PwDto>>
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string? DepartmentName { get; set; }
    }
}
