using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.SystemActors;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities
{
    public class EsWmsSieveProcessor
        (IOptions<SieveOptions> options,
        ISieveCustomFilterMethods customFilterMethods)
        : SieveProcessor(options, customFilterMethods)
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

            mapper.Property<Contractor>(x => x.ContractorId)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.VatId)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.ContractorName)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.City)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.Address)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.PostalCode)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.Region)
                .CanSort()
                .CanFilter();

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
            
            mapper.Property<WarehouseUnit>(x => x.WarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnit>(x => x.WarehouseUnitId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnit>(x => x.WarehouseUnitItems)
                .CanFilter()
                .HasName("FilterByWarehouseUnitItemIds");

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
            mapper.Property<WarehouseStock>(x => x.Value)
                .CanFilter()
                .CanSort();

            return mapper;
        }
    }

    public class SieveCustomFilterMethods : ISieveCustomFilterMethods
    {
        public IQueryable<PZ> DateFilter(IQueryable<PZ> source, string op, string[] values)
        {
            if (values.Length != 1 || !DateTime.TryParse(values[0], out DateTime dateValue))
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source.Where(item => item.DocumentIssueDate.Date == dateValue.Date);
                    break;
                case "!=":
                    source = source.Where(item => item.DocumentIssueDate.Date != dateValue.Date);
                    break;
                case ">":
                    source = source.Where(item => item.DocumentIssueDate.Date > dateValue.Date);
                    break;
                case "<":
                    source = source.Where(item => item.DocumentIssueDate.Date < dateValue.Date);
                    break;
                case ">=":
                    source = source.Where(item => item.DocumentIssueDate.Date >= dateValue.Date);
                    break;
                case "<=":
                    source = source.Where(item => item.DocumentIssueDate.Date <= dateValue.Date);
                    break;
                default:
                    break;
            }

            return source;
        }

        public IQueryable<WarehouseUnit> FilterByWarehouseUnitItemIds(IQueryable<WarehouseUnit> source, string op, string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return source;
            }

            switch (op)
            {
                case "==":
                    source = source.Where(unit => unit.WarehouseUnitItems.Any(item => values.Contains(item.WarehouseUnitItemId)));
                    break;
                default:
                    break;
            }

            return source;
        }
    }
}
