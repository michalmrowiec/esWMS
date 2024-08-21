using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.CreateMmm
{
    public class CreateMmmCommand
        : CreateBaseDocumentCommand, IRequest<BaseResponse<MmmDto>>
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string ToWarehouseId { get; set; } = null!;
        public bool DocumentItemsAreApproved { get; set; } = true;
    }
}
