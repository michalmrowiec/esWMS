using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities
{
    public class EsWmsSieveProcessor
        (IOptions<SieveOptions> options)
        : SieveProcessor(options)
    {
        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Product>(p => p.ProductCode)
                .CanSort()
                .CanFilter();

            mapper.Property<Product>(p => p.ProductName)
                .CanSort()
                .CanFilter();

            mapper.Property<Product>(p => p.Category.CategoryName)
                .CanSort()
                .CanFilter()
                .HasName("CategoryName");
            
            mapper.Property<Product>(p => p.Price)
                .CanSort()
                .CanFilter();

            mapper.Property<Product>(p => p.Unit)
                .CanSort()
                .CanFilter();

            return mapper;
        }
    }
}
