using esMWS.Domain.Entities.Documents;

namespace esWMS.Application.Functions.Documents.ZwFunctions
{
    public class ZwDto : BaseDocument
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string? DepartmentName { get; set; }
    }
}
