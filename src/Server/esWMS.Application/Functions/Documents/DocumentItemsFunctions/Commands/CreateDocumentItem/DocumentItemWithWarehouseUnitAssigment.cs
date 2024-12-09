namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    public record CreateDocumentWarehouseUnitItemCommand
        (string DocumentItemId,
        decimal Quantity,
        string? WarehouseUnitId = null,
        string? WarehouseUnitItemId = null,
        bool? IsMedia = null);
}
