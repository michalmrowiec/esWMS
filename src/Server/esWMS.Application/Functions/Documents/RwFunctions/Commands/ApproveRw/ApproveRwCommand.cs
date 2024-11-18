using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.RwFunctions.Commands.ApproveRw
{
    public class ApproveRwCommand : IRequest<BaseResponse<RwDto>>
    {
        public string DocumentId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
