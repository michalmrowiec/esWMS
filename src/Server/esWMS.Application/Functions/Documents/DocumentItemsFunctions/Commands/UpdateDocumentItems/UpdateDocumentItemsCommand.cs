using esWMS.Application.Functions.Documents.BaseDocumentFunctions;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.UpdateDocumentItems
{
    public class UpdateDocumentItemsCommand : IRequest<BaseResponse<BaseDocumentDto>>
    {
        public string DocumentId { get; set; } = null!;
        public IList<UpdateDocumentItemCommand> DocumentItems { get; set; } = [];
    }

    public class UpdateDocumentItemCommand
    {
        public string DocumentItemsId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public string? WarehouseUnitItemId { get; set; }
        public bool IsApproved { get; set; }
    }
}
