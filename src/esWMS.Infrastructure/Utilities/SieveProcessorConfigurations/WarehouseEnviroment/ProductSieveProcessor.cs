using esWMS.Domain.Entities.WarehouseEnviroment;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment
{
    internal class ProductSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Product>(x => x.ProductCode)
                .CanSort()
                .CanFilter();
            mapper.Property<Product>(x => x.ProductName)
                .CanSort()
                .CanFilter();
            mapper.Property<Product>(x => x.CategoryId)
                .CanSort()
                .CanFilter();
            mapper.Property<Product>(x => x.Category.CategoryName)
                .CanSort()
                .CanFilter()
                .HasName("CategoryName");
            mapper.Property<Product>(x => x.Price)
                .CanSort()
                .CanFilter();
            mapper.Property<Product>(x => x.Unit)
                .CanSort()
                .CanFilter();
            mapper.Property<Product>(x => x.IsMedia)
                .CanSort()
                .CanFilter();
        }
    }
}
