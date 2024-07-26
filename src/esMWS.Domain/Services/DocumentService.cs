using esMWS.Domain.Entities.Documents;

namespace esMWS.Domain.Services
{
    public static class DocumentService
    {
        public static string GenerateDocumentId(this BaseDocument document, int documentNumber)
        {
            if (documentNumber < 1 || documentNumber > 999)
            {
                throw new ArgumentException("The document number must have a value between 1 and 999", nameof(documentNumber));
            }

            if (string.IsNullOrEmpty(document.IssueWarehouseId))
            {
                throw new ArgumentException("The IssueWarehouseId must not be null or empty", nameof(document.IssueWarehouseId));
            }

            if (document.DocumentIssueDate == default)
            {
                throw new ArgumentException("The DocumentIssueDate must be a valid date", nameof(document.DocumentIssueDate));
            }

            DateTime issueDate = document.DocumentIssueDate;
            string documentType = document.GetType().Name;

            return $"{documentType}/{document.IssueWarehouseId}/{issueDate.Year}/{issueDate.Month:D2}/{issueDate.Day:D2}/{documentNumber:D3}";
        }
    }
}
