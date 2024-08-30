using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.ApproveZw
{
    public class ApproveZwCommand : IRequest<BaseResponse<ZwDto>>
    {
        public string DocumentId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
