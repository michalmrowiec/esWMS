using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Domain.Entities.Documents
{
    public class MMP : BaseDocument
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string FromWarehouseId { get; set; } = null!;
        public string RelatedMmmId { get; set; } = null!;

        public Warehouse? FromWarehouse { get; set; }
        public MMM? RelatedMmm { get; set; }

        public MMP()
        { }

        public MMP
            (string documentId,
            string issueWarehouseId,
            string? comment,
            DateTime documentIssueDate,
            string? issuingEmployeeId,
            string? assignedEmployeeId,
            bool isApproved,
            DateTime? aprovedDate,
            string? approvingEmployeeId,
            DateTime? goodsReceiptDate,
            string fromWarehouseId,
            string relatedMmmId,
            DateTime createdAt,
            string? createdBy,
            DateTime? modifiedAt,
            string? modifiedBy)
            : base(documentId,
                  issueWarehouseId,
                  comment,
                  documentIssueDate,
                  issuingEmployeeId,
                  assignedEmployeeId,
                  isApproved,
                  aprovedDate,
                  approvingEmployeeId,
                  createdAt,
                  createdBy,
                  modifiedAt,
                  modifiedBy)
        {
            GoodsReceiptDate = goodsReceiptDate;
            FromWarehouseId = fromWarehouseId;
            RelatedMmmId = relatedMmmId;
        }
    }
}
