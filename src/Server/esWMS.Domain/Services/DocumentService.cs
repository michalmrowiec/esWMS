using esWMS.Domain.Entities.Documents;
using System.Text.RegularExpressions;

namespace esWMS.Domain.Services
{
    public static class DocumentService
    {
        public static string GenerateDocumentId(this BaseDocument document, string[] documentIds)
        {
            if (documentIds == null)
            {
                throw new ArgumentException("Document IDs cannot be null", nameof(documentIds));
            }

            string pattern = @"^([A-Z]{2}[+-]|[A-Z]{2,3})/([A-Z]|\d){3}/\d{4}/[0-1]\d/[0-3]\d/\d{3}$";
            var regex = new Regex(pattern);

            int documentNumber = 0;

            if (documentIds.Length > 0)
            {
                documentNumber = documentIds
                .Select(x =>
                {
                    if (!regex.IsMatch(x))
                    {
                        throw new ArgumentException($"Invalid document Id format: {x}");
                    }

                    return int.Parse(new string(x.TakeLast(3).ToArray()));
                })
                .Max() + 1;
            }
            else
            {
                documentNumber = 1;
            }

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

            switch(documentType)
            {
                case nameof(MMP):
                    documentType = "MM+";
                    break;
                case nameof(MMM):
                    documentType = "MM-";
                    break;
                default:
                    break;
            }

            return $"{documentType}/{document.IssueWarehouseId}/{issueDate.Year}/{issueDate.Month:D2}/{issueDate.Day:D2}/{documentNumber:D3}";
        }
    }
}
