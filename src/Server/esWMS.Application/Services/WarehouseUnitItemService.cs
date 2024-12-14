using esWMS.Application.Functions.Documents.DocumentItemsFunctions.Commands.CreateDocumentItem;
using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Services
{
    internal static class WarehouseUnitItemService
    {
        internal static WarehouseUnitItem CreateWarehouseUnitItem
            (WarehouseUnit warehouseUnit, DocumentItem docItem, ReceivingItemAssignment itemAssignment, string? modifiedBy)
        {
            return new WarehouseUnitItem(
                warehouseUnitId: warehouseUnit.WarehouseUnitId,
                productId: docItem.ProductId,
                quantity: itemAssignment.Quantity,
                blockedQuantity: itemAssignment.Quantity,
                bestBefore: docItem.BestBefore,
                batchLot: docItem.BatchLot,
                serialNumber: docItem.SerialNumber,
                price: docItem.Price,
                createdBy: modifiedBy,
                isMediaOfWarehouseUnit: itemAssignment.IsMedia ?? false);
        }

        internal static DocumentWarehouseUnitItem CreateDocumentWarehouseUnitItem
            (DocumentItem docItem, WarehouseUnitItem warehouseUnitItem, ReceivingItemAssignment itemAssignment, string? modifiedBy)
        {
            return new DocumentWarehouseUnitItem
            {
                DocumentWarehouseUnitItemId = Guid.NewGuid().ToString(),
                DocumentItemId = docItem.DocumentItemId,
                WarehouseUnitItemId = warehouseUnitItem.WarehouseUnitItemId,
                Quantity = itemAssignment.Quantity,
                CreatedAt = DateTime.Now,
                CreatedBy = modifiedBy
            };
        }
    }
}
