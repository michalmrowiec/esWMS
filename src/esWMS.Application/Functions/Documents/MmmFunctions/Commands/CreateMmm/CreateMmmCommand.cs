using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Functions.Documents.BaseDocumentFunctions.Command;
using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;
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

        public List<WarehouseUnit> WarehouseUnits { get; set; } = [];
    }
}
