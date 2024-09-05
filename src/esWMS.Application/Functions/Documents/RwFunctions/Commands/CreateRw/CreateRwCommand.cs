using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.RwFunctions.Commands.CreateRw
{
    public class CreateRwCommand
        : CreateBaseDocumentCommand, IRequest<BaseResponse<RwDto>>
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string? DepartmentName { get; set; }
    }
}
