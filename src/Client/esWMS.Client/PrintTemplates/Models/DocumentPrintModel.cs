﻿using esWMS.Client.ViewModels.Documents;
using esWMS.Client.ViewModels.SystemActors;
using esWMS.Client.ViewModels.WarehouseEnvironment.Warehouse;

namespace esWMS.Client.PrintTemplates.Models
{
    public class DocumentPrintModel
    {
        public DocumentPrintModel(
            BaseDocumentVM model,
            ContractorVM contractor,
            DocumentTypesName documentType,
            DocumentContractorType contractorType)
        {
            Model = model;
            DocumentType = documentType;
            ContractorType = contractorType;
            IssueWarehouseName = model.IssueWarehouse?.WarehouseName ?? "";
            IssueWarehouseAddress = model.IssueWarehouse?.Address ?? "";
            IssueWarehousePostalCodeCity = $"{model.IssueWarehouse?.PostalCode} {model.IssueWarehouse?.City}";
            IssueWarehouseVatId = "";
            ContractorName = contractor.ContractorName;
            ContractorAddress = contractor.Address ?? "";
            ContractorPostalCodeCity = $"{contractor.PostalCode} {contractor.City}";
            ContractorVatId = contractor?.VatId ?? "";
        }

        public DocumentPrintModel(
            BaseDocumentVM model,
            WarehouseVM targetWarehouse,
            DocumentTypesName documentType,
            DocumentContractorType contractorType = DocumentContractorType.Receiver)
        {
            Model = model;
            DocumentType = documentType;
            ContractorType = contractorType;
            IssueWarehouseName = model.IssueWarehouse?.WarehouseName ?? "";
            IssueWarehouseAddress = model.IssueWarehouse?.Address ?? "";
            IssueWarehousePostalCodeCity = $"{model.IssueWarehouse?.PostalCode} {model.IssueWarehouse?.City}";
            IssueWarehouseVatId = "";
            ContractorName = targetWarehouse.WarehouseName;
            ContractorAddress = targetWarehouse.Address ?? "";
            ContractorPostalCodeCity = $"{targetWarehouse.PostalCode} {targetWarehouse.City}";
            ContractorVatId = "";
        }

        public DocumentPrintModel(
            BaseDocumentVM model,
            string targetOrSourceName,
            DocumentTypesName documentType,
            DocumentContractorType contractorType)
        {
            Model = model;
            DocumentType = documentType;
            ContractorType = contractorType;
            IssueWarehouseName = model.IssueWarehouse?.WarehouseName ?? "";
            IssueWarehouseAddress = model.IssueWarehouse?.Address ?? "";
            IssueWarehousePostalCodeCity = $"{model.IssueWarehouse?.PostalCode} {model.IssueWarehouse?.City}";
            IssueWarehouseVatId = "";
            ContractorName = targetOrSourceName;
        }

        public BaseDocumentVM Model { get; set; }
        public DocumentTypesName DocumentType { get; set; }
        public DocumentContractorType ContractorType { get; set; }
        public string IssueWarehouseName { get; set; }
        public string IssueWarehouseAddress { get; set; }
        public string IssueWarehousePostalCodeCity { get; set; }
        public string IssueWarehouseVatId { get; set; }
        public string ContractorName { get; set; }
        public string? ContractorAddress { get; set; }
        public string? ContractorPostalCodeCity { get; set; }
        public string? ContractorVatId { get; set; }
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
        Supplier,
        Source,
        Target
    }
}
