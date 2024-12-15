using esWMS.Domain.Entities.WarehouseEnvironment;
using esWMS.Infrastructure.Repositories.WarehouseEnvironment;
using esWMS.Infrastructure.UnitTests.Helpers;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Sieve.Models;

namespace esWMS.Infrastructure.UnitTests.Repositories.WarehouseUnitItems
{
    public class WarehouseUnitItemsRepositoryTests
    {
        [Theory]
        [MemberData(
            nameof(WarehouseUnitItemsRepositoryTestsData.ValidData),
            MemberType = typeof(WarehouseUnitItemsRepositoryTestsData))]
        public async Task GetWarehouseUnitItemsByIdsAsync_ForValidData_ReturnWarehouseUnitItemsWithBlockedQuantity(
            Category[] categories,
            Product[] products,
            Warehouse warehouse,
            Dictionary<string, decimal> warehouseUnitItemIdsQuantity,
            List<WarehouseUnitItem> warehouseUnitItems)
        {
            await using var context = EsWMSContextInMemoryFactory.Create();

            var logger = new Mock<ILogger<WarehouseUnitItemRepository>>();

            IOptions<SieveOptions> sieveOptions = Options.Create(new SieveOptions());

            var warehouseUnitItemRepository =
                new WarehouseUnitItemRepository(
                    context,
                    logger.Object,
                    new EsWmsSieveProcessor(
                        sieveOptions,
                        new SieveCustomFilterMethods(),
                        new SieveCustomSortMethods()));

            context.Categories.AddRange(categories);
            context.Products.AddRange(products);
            context.Warehouses.Add(warehouse);
            await context.SaveChangesAsync();

            var warehouseUnitItemsWithBlockedQuantity =
                await warehouseUnitItemRepository.BlockExistWarehouseUnitItemsQuantityAsync(warehouseUnitItemIdsQuantity);

            warehouseUnitItemsWithBlockedQuantity.Should().BeEquivalentTo(warehouseUnitItems, opt =>
                opt.Excluding(item => item.WarehouseUnit)
                   .Excluding(item => item.Product));
        }
    }
}
