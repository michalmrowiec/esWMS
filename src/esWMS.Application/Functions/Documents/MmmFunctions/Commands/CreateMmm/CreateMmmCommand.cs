using esWMS.Application.Functions.WarehouseUnits.Commands.UpdateWarehouseUnit;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.CreateMmm
{
    public class CreateMmmCommand
        : IRequest<BaseResponse<MmmDto>>
    {
        public string IssueWarehouseId { get; set; } = null!;
        public string? Comment { get; set; }
        public DateTime DocumentIssueDate { get; set; }
        public string? IssuingEmployeeId { get; set; }
        public string? AssignedEmployeeId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? GoodsReleaseDate { get; set; }
        public string ToWarehouseId { get; set; } = null!;

        public List<UpdateWarehouseUnitCommand> WarehouseUnits { get; set; } = [];
    }
}
