namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    public abstract record DocumentWarehouseUnitItemBase(
        string DocumentItemId,
        decimal Quantity,
        bool? IsMedia = null);

    public record ReceivingItemAssignment(
        string DocumentItemId,
        decimal Quantity,
        string WarehouseUnitId,
        bool? IsMedia = null) : DocumentWarehouseUnitItemBase(DocumentItemId, Quantity, IsMedia);

    public record CreateDocumentWarehouseUnitItemCommand(
        string DocumentItemId,
        decimal Quantity,
        string? WarehouseUnitId = null,
        string? WarehouseUnitItemId = null,
        bool? IsMedia = null) : DocumentWarehouseUnitItemBase(DocumentItemId, Quantity, IsMedia);
}
