using esWMS.Domain.Entities.WarehouseEnviroment;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment
{
    internal class WarehouseUnitSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<WarehouseUnit>(x => x.WarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnit>(x => x.WarehouseUnitId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnit>(x => x.IsBlocked)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnit>(x => x.LocationId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnit>(x => x.CanBeStacked)
                .CanSort()
                .CanFilter();
        }

        public static IQueryable<WarehouseUnit> AnyBlockedItem(
            IQueryable<WarehouseUnit> source, string op, string[] values)
        {
            if (values == null || values.Length == 0 || !bool.TryParse(values[0], out bool anyBlockedItem))
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    if (anyBlockedItem)
                        source = source.Where(wu => wu.WarehouseUnitItems.Any(wui => wui.BlockedQuantity > 0));
                    else
                        source = source.Where(wu => wu.WarehouseUnitItems.All(wui => wui.BlockedQuantity == 0));
                    break;
                case "!=":
                    if (anyBlockedItem)
                        source = source.Where(wu => wu.WarehouseUnitItems.All(wui => wui.BlockedQuantity == 0));
                    else
                        source = source.Where(wu => wu.WarehouseUnitItems.Any(wui => wui.BlockedQuantity > 0));
                    break;
                default:
                    break;
            }

            return source;
        }

        public static IQueryable<WarehouseUnit> ProductName(
            IQueryable<WarehouseUnit> source, string op, string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return source;
            }

            var value = values.First();

            switch (op)
            {
                case "@=":
                    source = source
                        .Where(wu => wu.WarehouseUnitItems.Any(wui => wui.Product.ProductName.Contains(value)));
                    break;
                case "!@=":
                    source = source
                        .Where(wu => wu.WarehouseUnitItems.Any(wui => !wui.Product.ProductName.Contains(value)));
                    break;
                case "==":
                    source = source
                        .Where(wu => wu.WarehouseUnitItems.Any(wui => wui.Product.ProductName == value));
                    break;
                case "!=":
                    source = source
                        .Where(wu => wu.WarehouseUnitItems.All(wui => wui.Product.ProductName != value));
                    break;
                default:
                    break;
            }

            return source;
        }
    }
}
