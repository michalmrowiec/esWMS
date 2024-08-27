namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    public record CreateDocumentWarehouseUnitItemCommand
        (string DocumentItemId, int Quantity, string? WarehouseUnitId = null, string? WarehouseUnitItemId = null);

}
