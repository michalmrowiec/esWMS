using esMWS.Domain.Entities.WarehouseEnviroment;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment
{
    internal class WarehouseUnitItemSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<WarehouseUnitItem>(x => x.WarehouseUnitItemId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnitItem>(x => x.WarehouseUnitId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnitItem>(x => x.ProductId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnitItem>(x => x.Quantity)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnitItem>(x => x.BlockedQuantity)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnitItem>(x => x.BatchLot)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnitItem>(x => x.SerialNumber)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnitItem>(x => x.Product.ProductName)
                .CanSort()
                .CanFilter()
                .HasName("ProductName");
            mapper.Property<WarehouseUnitItem>(x => x.WarehouseUnit.WarehouseId)
                .CanSort()
                .CanFilter()
                .HasName("WarehouseId");
            mapper.Property<WarehouseUnitItem>(x => x.Price)
                .CanSort()
                .CanFilter();
        }
    }
}
