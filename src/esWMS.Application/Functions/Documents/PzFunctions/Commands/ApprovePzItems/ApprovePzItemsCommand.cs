using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePzItems
{
    public class ApprovePzItemsCommand : IRequest<BaseResponse<PzDto>>
    {
        public string DocumentId { get; set; } = null!;
        public List<DocWithAssigment> DocumentItemsWithAssigment { get; set; } = null!;

        public string? CreatedBy { get; set; }
    }

    public record DocWithAssigment(string DocumentItemId, string? WarehouseUnitId);
}
