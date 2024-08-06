using esMWS.Domain.Entities.Documents;
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
            mapper.Property<Product>(x => x.ProductCode)
                .CanSort()
                .CanFilter();

            mapper.Property<Product>(x => x.ProductName)
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

            mapper.Property<Warehouse>
                (x => x.WarehouseId)
                .CanSort()
                .CanFilter();

            mapper.Property<Warehouse>(x => x.WarehouseName)
                .CanSort()
                .CanFilter();

            mapper.Property<Warehouse>(x => x.City)
                .CanSort()
                .CanFilter();

            mapper.Property<Warehouse>(x => x.Address)
                .CanSort()
                .CanFilter();

            mapper.Property<Warehouse>(x => x.PostalCode)
                .CanSort()
                .CanFilter();

            mapper.Property<Warehouse>(x => x.Region)
                .CanSort()
                .CanFilter();

            mapper.Property<PZ>(x => x.DocumentId)
                .CanSort()
                .CanFilter();

            mapper.Property<PZ>(x => x.IssueWarehouseId)
                .CanSort()
                .CanFilter();

            mapper.Property<PZ>(x => x.DocumentIssueDate)
                .CanSort()
                .CanFilter();

            mapper.Property<PZ>(x => x.IsApproved)
                .CanSort()
                .CanFilter();

            mapper.Property<PZ>(x => x.SupplierContractorId)
                .CanSort()
                .CanFilter();

            return mapper;
        }
    }
}
