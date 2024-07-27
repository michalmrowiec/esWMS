using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePzItems
{
    public class ApprovePzItemsCommand : IRequest<BaseResponse<PzDto>>
    {
        public string DocumentId { get; set; } = null!;
        public string[] DocumentItemsId { get; set; } = null!;

    }
}
