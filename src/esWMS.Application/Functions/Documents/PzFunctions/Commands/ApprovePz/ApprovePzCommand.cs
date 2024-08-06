using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePz
{
    public class ApprovePzCommand : IRequest<BaseResponse<PzDto>>
    {
        public string DocumentId { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
