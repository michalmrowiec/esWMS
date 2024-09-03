using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.SystemActors;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations
{
    public class EsWmsSieveProcessor
        (IOptions<SieveOptions> options,
        ISieveCustomFilterMethods customFilterMethods)
        : SieveProcessor(options, customFilterMethods)
    {
        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            new ProductSieveProcessor().Configure(mapper);

            new CategorySieveProcessor().Configure(mapper);

            new ContractorSieveProcessor().Configure(mapper);

            new LocationSieveProcessor().Configure(mapper);

            new WarehouseSieveProcessor().Configure(mapper);

            new WarehouseUnitSieveProcessor().Configure(mapper);

            new WarehouseUnitItemSieveProcessor().Configure(mapper);

            new WarehouseStockSieveProcessor().Configure(mapper);

            new DocumentItemsSieveProcessor().Configure(mapper);

            new PzSieveProcessor().Configure(mapper);

            new WzSieveProcessor().Configure(mapper);

            new MmmSieveProcessor().Configure(mapper);

            new MmpSieveProcessor().Configure(mapper);

            new PwSieveProcessor().Configure(mapper);

            new RwSieveProcessor().Configure(mapper);

            new ZwSieveProcessor().Configure(mapper);

            return mapper;
        }
    }

    public class SieveCustomFilterMethods : ISieveCustomFilterMethods
    {
        public IQueryable<PZ> DocumentIssueDate(IQueryable<PZ> source, string op, string[] values) =>
            new BaseDocumentSieveProcessor().DocumentIssueDateFilter(source, op, values);
        public IQueryable<WZ> DocumentIssueDate(IQueryable<WZ> source, string op, string[] values) =>
            new BaseDocumentSieveProcessor().DocumentIssueDateFilter(source, op, values);
        public IQueryable<MMM> DocumentIssueDate(IQueryable<MMM> source, string op, string[] values) =>
            new BaseDocumentSieveProcessor().DocumentIssueDateFilter(source, op, values);
        public IQueryable<MMP> DocumentIssueDate(IQueryable<MMP> source, string op, string[] values) =>
            new BaseDocumentSieveProcessor().DocumentIssueDateFilter(source, op, values);
        public IQueryable<PW> DocumentIssueDate(IQueryable<PW> source, string op, string[] values) =>
            new BaseDocumentSieveProcessor().DocumentIssueDateFilter(source, op, values);
        public IQueryable<RW> DocumentIssueDate(IQueryable<RW> source, string op, string[] values) =>
            new BaseDocumentSieveProcessor().DocumentIssueDateFilter(source, op, values);
        public IQueryable<ZW> DocumentIssueDate(IQueryable<ZW> source, string op, string[] values) =>
            new BaseDocumentSieveProcessor().DocumentIssueDateFilter(source, op, values);

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
