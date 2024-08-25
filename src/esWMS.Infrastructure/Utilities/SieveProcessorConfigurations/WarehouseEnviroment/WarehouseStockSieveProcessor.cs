using esMWS.Domain.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment
{
    internal class WarehouseStockSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<WarehouseStock>(x => x.ProductId)
                .CanFilter()
                .CanSort();
            mapper.Property<WarehouseStock>(x => x.ProductName)
                .CanFilter()
                .CanSort();
            mapper.Property<WarehouseStock>(x => x.CategoryId)
                .CanFilter()
                .CanSort();
            mapper.Property<WarehouseStock>(x => x.CategoryName)
                .CanFilter()
                .CanSort();
            mapper.Property<WarehouseStock>(x => x.Quantity)
                .CanFilter()
                .CanSort();
            mapper.Property<WarehouseStock>(x => x.BlockedQuantity)
                .CanFilter()
                .CanSort();
            mapper.Property<WarehouseStock>(x => x.Value)
                .CanFilter()
                .CanSort();
        }
    }
}
