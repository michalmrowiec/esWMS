using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.ApproveWz
{
    public class ApproveWzCommand : IRequest<BaseResponse<WzDto>>
    {
        public string DocumentId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
