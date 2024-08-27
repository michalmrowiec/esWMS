using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.ApprovePw
{
    public class ApprovePwCommand : IRequest<BaseResponse<PwDto>>
    {
        public string DocumentId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
