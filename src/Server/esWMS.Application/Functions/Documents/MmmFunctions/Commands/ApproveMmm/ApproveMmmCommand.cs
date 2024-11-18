using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.ApproveMmm
{
    public class ApproveMmmCommand : IRequest<BaseResponse<MmmDto>>
    {
        public string DocumentId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
