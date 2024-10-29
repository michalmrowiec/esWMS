using esWMS.Client.ViewModels.Documents;

namespace esWMS.Client.PrintTemplates.Document
{
    public class DocumentPrintModel
    {
        public DocumentPrintModel(
            BaseDocumentVM model,
            DocumentTypesName documentType,
            DocumentContractorType contractorType,
            string issueWarehouseName,
            string issueWarehouseAddress,
            string issueWarehousePostalCodeCity,
            string issueWarehouseVatId,
            string contractorName,
            string contractorAddress,
            string contractorPostalCodeCity,
            string contractorVatId)
        {
            Model = model;
            DocumentType = documentType;
            ContractorType = contractorType;
            IssueWarehouseName = issueWarehouseName;
            IssueWarehouseAddress = issueWarehouseAddress;
            IssueWarehousePostalCodeCity = issueWarehousePostalCodeCity;
            IssueWarehouseVatId = issueWarehouseVatId;
            ContractorName = contractorName;
            ContractorAddress = contractorAddress;
            ContractorPostalCodeCity = contractorPostalCodeCity;
            ContractorVatId = contractorVatId;
        }

        public BaseDocumentVM Model { get; set; }
        public DocumentTypesName DocumentType { get; set; }
        public DocumentContractorType ContractorType { get; set; }
        public string IssueWarehouseName { get; set; }
        public string IssueWarehouseAddress { get; set; }
        public string IssueWarehousePostalCodeCity { get; set; }
        public string IssueWarehouseVatId { get; set; }
        public string ContractorName { get; set; }
        public string ContractorAddress { get; set; }
        public string ContractorPostalCodeCity { get; set; }
        public string ContractorVatId { get; set; }
    }

    public enum DocumentTypesName
    {
        PZ,
        WZ,
        PW,
        RW,
        ZW,
        MMP,
        MMM
    }

    public enum DocumentContractorType
    {
        Receiver,
        Supplier
    }
}
