namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    public record ReceivingItemAssignment
        (string DocumentItemId,
        int Quantity,
        string WarehouseUnitId,
        bool? IsMedia = null);
}
