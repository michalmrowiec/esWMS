namespace esWMS.Client.ViewModels.Documents
{
    public class PzVM : BaseDocumentVM
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string SupplierContractorId { get; set; } = null!;

        public ContractorVM? SupplierContractor { get; set; }
    }
}
