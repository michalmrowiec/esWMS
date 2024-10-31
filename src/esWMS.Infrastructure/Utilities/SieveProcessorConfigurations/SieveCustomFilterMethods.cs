using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Models;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations
{
    public class SieveCustomFilterMethods : ISieveCustomFilterMethods
    {
        public IQueryable<PZ> DocumentIssueDate(
            IQueryable<PZ> source, string op, string[] values) =>
            BaseDocumentSieveProcessor.DocumentIssueDateFilter(source, op, values);
        public IQueryable<WZ> DocumentIssueDate(
            IQueryable<WZ> source, string op, string[] values) =>
            BaseDocumentSieveProcessor.DocumentIssueDateFilter(source, op, values);
        public IQueryable<MMM> DocumentIssueDate(
            IQueryable<MMM> source, string op, string[] values) =>
            BaseDocumentSieveProcessor.DocumentIssueDateFilter(source, op, values);
        public IQueryable<MMP> DocumentIssueDate(
            IQueryable<MMP> source, string op, string[] values) =>
            BaseDocumentSieveProcessor.DocumentIssueDateFilter(source, op, values);
        public IQueryable<PW> DocumentIssueDate(
            IQueryable<PW> source, string op, string[] values) =>
            BaseDocumentSieveProcessor.DocumentIssueDateFilter(source, op, values);
        public IQueryable<RW> DocumentIssueDate(
            IQueryable<RW> source, string op, string[] values) =>
            BaseDocumentSieveProcessor.DocumentIssueDateFilter(source, op, values);
        public IQueryable<ZW> DocumentIssueDate(
            IQueryable<ZW> source, string op, string[] values) =>
            BaseDocumentSieveProcessor.DocumentIssueDateFilter(source, op, values);

        public IQueryable<WarehouseUnit> AnyBlockedItem(
            IQueryable<WarehouseUnit> source, string op, string[] values) =>
            WarehouseUnitSieveProcessor.AnyBlockedItem(source, op, values);

        public IQueryable<WarehouseUnit> ProductName
            (IQueryable<WarehouseUnit> source, string op, string[] values) =>
            WarehouseUnitSieveProcessor.ProductName(source, op, values);

        public IQueryable<WarehouseUnit> ItemCount
            (IQueryable<WarehouseUnit> source, string op, string[] values)
        {
            if (values == null || values.Length == 0 || !int.TryParse(values[0], out int itemCount))
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source.Where(warehouseUnit => warehouseUnit.WarehouseUnitItems.Count == itemCount);
                    break;
                case "!=":
                    source = source.Where(warehouseUnit => warehouseUnit.WarehouseUnitItems.Count != itemCount);
                    break;
                case ">":
                    source = source.Where(warehouseUnit => warehouseUnit.WarehouseUnitItems.Count > itemCount);
                    break;
                case "<":
                    source = source.Where(warehouseUnit => warehouseUnit.WarehouseUnitItems.Count < itemCount);
                    break;
                case ">=":
                    source = source.Where(warehouseUnit => warehouseUnit.WarehouseUnitItems.Count >= itemCount);
                    break;
                case "<=":
                    source = source.Where(warehouseUnit => warehouseUnit.WarehouseUnitItems.Count <= itemCount);
                    break;
                default:
                    break;
            }

            return source;
        }

        public IQueryable<Location> IsFull
            (IQueryable<Location> source, string op, string[] values)
        {
            if (values == null || values.Length == 0 || !bool.TryParse(values[0], out bool isFull))
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    if (isFull)
                        source = source.Where(location => location.Capacity <= location.WarehouseUnits.Count);
                    else
                        source = source.Where(location => location.Capacity > location.WarehouseUnits.Count);
                    break;
                case "!=":
                    if (isFull)
                        source = source.Where(location => location.Capacity > location.WarehouseUnits.Count);
                    else
                        source = source.Where(location => location.Capacity <= location.WarehouseUnits.Count);
                    break;
                default:
                    break;
            }

            return source;
        }

        public IQueryable<RW> ContainsProductIds
            (IQueryable<RW> source, string op, string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source
                        .Where(rw => rw.DocumentItems.Any(di => values.Contains(di.ProductId)));
                    break;
                default:
                    break;
            }

            return source;
        }

        public IQueryable<ZW> ContainsProductIds
            (IQueryable<ZW> source, string op, string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source
                        .Where(zw => zw.DocumentItems.Any(di => values.Contains(di.ProductId)));
                    break;
                default:
                    break;
            }

            return source;
        }

        public IQueryable<WarehouseUnit> FilterByWarehouseUnitItemIds
            (IQueryable<WarehouseUnit> source, string op, string[] values)
        {
            if (values == null
                || values.Length == 0)
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source.Where(unit => unit.WarehouseUnitItems
                                            .Any(item => values.Contains(item.WarehouseUnitItemId)));
                    break;
                default:
                    break;
            }

            return source;
        }

        public IQueryable<DocumentItem> DocumentItemId
            (IQueryable<DocumentItem> source, string op, string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source.Where(item => values.Contains(item.DocumentItemId));
                    break;
                default:
                    break;
            }

            return source;
        }

        public IQueryable<Product> ProductId
            (IQueryable<Product> source, string op, string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source.Where(item => values.Contains(item.ProductId));
                    break;
                default:
                    break;
            }

            return source;
        }

        public IQueryable<WarehouseUnitItem> WarehouseUnitItemId
            (IQueryable<WarehouseUnitItem> source, string op, string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source.Where(item => values.Contains(item.WarehouseUnitItemId));
                    break;
                case "!=":
                    source = source.Where(item => !values.Contains(item.WarehouseUnitItemId));
                    break;
                default:
                    break;
            }

            return source;
        }

        public IQueryable<WarehouseUnitItem> Available
            (IQueryable<WarehouseUnitItem> source, string op, string[] values)
        {
            if (values == null
                || values.Length == 0
                || !int.TryParse(values[0], out int intValue))
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity == intValue);
                    break;
                case "!=":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity != intValue);
                    break;
                case ">":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity > intValue);
                    break;
                case "<":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity < intValue);
                    break;
                case ">=":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity >= intValue);
                    break;
                case "<=":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity <= intValue);
                    break;
                default:
                    break;
            }

            return source;
        }

        public IQueryable<WarehouseStock> Available
            (IQueryable<WarehouseStock> source, string op, string[] values)
        {
            if (values == null
                || values.Length == 0
                || !int.TryParse(values[0], out int intValue))
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity == intValue);
                    break;
                case "!=":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity != intValue);
                    break;
                case ">":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity > intValue);
                    break;
                case "<":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity < intValue);
                    break;
                case ">=":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity >= intValue);
                    break;
                case "<=":
                    source = source.Where(item => item.Quantity - item.BlockedQuantity <= intValue);
                    break;
                default:
                    break;
            }

            return source;
        }
    }
}
