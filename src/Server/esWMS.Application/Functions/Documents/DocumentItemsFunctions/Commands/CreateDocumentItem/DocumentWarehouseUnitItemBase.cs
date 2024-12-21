namespace esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem
{
    public abstract class DocumentWarehouseUnitItemBase
    {
        public string DocumentItemId { get; }
        public decimal Quantity { get; }
        public bool? IsMedia { get; }

        protected DocumentWarehouseUnitItemBase(string documentItemId, decimal quantity, bool? isMedia = null)
        {
            DocumentItemId = documentItemId;
            Quantity = quantity;
            IsMedia = isMedia;
        }
    }

    public class ReceivingItemAssignment : DocumentWarehouseUnitItemBase
    {
        public string WarehouseUnitId { get; }

        public ReceivingItemAssignment(string documentItemId, decimal quantity, string warehouseUnitId, bool? isMedia = null)
            : base(documentItemId, quantity, isMedia)
        {
            WarehouseUnitId = warehouseUnitId;
        }
    }

    public class CreateDocumentWarehouseUnitItemCommand : DocumentWarehouseUnitItemBase
    {
        public string? WarehouseUnitId { get; }
        public string? WarehouseUnitItemId { get; }

        public CreateDocumentWarehouseUnitItemCommand(
            string documentItemId,
            decimal quantity,
            string? warehouseUnitId = null,
            string? warehouseUnitItemId = null,
            bool? isMedia = null)
            : base(documentItemId, quantity, isMedia)
        {
            WarehouseUnitId = warehouseUnitId;
            WarehouseUnitItemId = warehouseUnitItemId;
        }
    }
}
