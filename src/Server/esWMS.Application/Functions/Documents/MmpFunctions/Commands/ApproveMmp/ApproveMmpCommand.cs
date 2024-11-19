using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmpFunctions.Commands.ApproveMmp
{
    public class ApproveMmpCommand : IRequest<BaseResponse<MmpDto>>
    {
        public string DocumentId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
