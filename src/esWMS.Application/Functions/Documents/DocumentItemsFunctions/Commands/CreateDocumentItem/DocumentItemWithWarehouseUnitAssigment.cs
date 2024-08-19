namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    public record CreateDocumentWarehouseUnitItemCommand
        (string DocumentItemId, string WarehouseUnitId, int Quantity, string? WarehouseUnitItemId = null);

}
