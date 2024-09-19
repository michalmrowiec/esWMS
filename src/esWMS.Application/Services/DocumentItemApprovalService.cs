using esMWS.Domain.Entities.Documents;

namespace esWMS.Application.Services
{
    internal static class DocumentItemApprovalService
    {
        internal static void ApproveDocumentItems(this BaseDocument document, string? modifiedBy)
        {
            foreach (var documentItem in document.DocumentItems)
            {
                var totalQuantitySoFar = documentItem.DocumentWarehouseUnitItems.Sum(x => x.Quantity);

                if (totalQuantitySoFar == documentItem.Quantity)
                {
                    documentItem.IsApproved = true;
                }

                documentItem.ModifiedBy = modifiedBy;
                documentItem.ModifiedAt = DateTime.Now;
            }
        }
    }
}
