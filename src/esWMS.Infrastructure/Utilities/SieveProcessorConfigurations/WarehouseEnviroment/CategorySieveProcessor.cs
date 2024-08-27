using esMWS.Domain.Entities.WarehouseEnviroment;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment
{
    internal class CategorySieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Category>(x => x.CategoryId)
                .CanSort()
                .CanFilter();
            mapper.Property<Category>(x => x.ParentCategoryId)
                .CanSort()
                .CanFilter();
            mapper.Property<Category>(x => x.CategoryName)
                .CanSort()
                .CanFilter();
            mapper.Property<Category>(x => x.ParentCategory.CategoryName)
                .CanSort()
                .CanFilter()
                .HasName("ParentCategoryName");
        }
    }
}
