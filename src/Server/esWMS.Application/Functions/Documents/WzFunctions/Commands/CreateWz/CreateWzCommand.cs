using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.CreateWz
{
    public class CreateWzCommand
        : CreateBaseDocumentCommand, IRequest<BaseResponse<WzDto>>
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string RecipientContractorId { get; set; } = null!;
    }
}
