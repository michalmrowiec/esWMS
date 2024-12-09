namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    public record ReceivingItemAssignment
        (string DocumentItemId,
        decimal Quantity,
        string WarehouseUnitId,
        bool? IsMedia = null);
}
