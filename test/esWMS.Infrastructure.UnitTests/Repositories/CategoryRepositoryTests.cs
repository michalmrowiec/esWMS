using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Infrastructure.Repositories.WarehouseEnvironment;
using esWMS.Infrastructure.UnitTests.Helpers;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Sieve.Models;

namespace esWMS.Infrastructure.UnitTests.Repositories
{
    public class CategoryRepositoryTests
    {
        [Theory]
        [MemberData(
            nameof(CategoryRepositoryTestsData.ValidData),
            MemberType = typeof(CategoryRepositoryTestsData))]
        public async Task CategoryRepositoryCreate_ForValidData_CreatesCategories(Category category)
        {
            await using var context = EsWMSContextInMemoryFactory.Create();

            var logger = new Mock<ILogger<CategoryRepository>>();

            IOptions<SieveOptions> sieveOptions = Options.Create(new SieveOptions());

            var categoryRepository =
                new CategoryRepository(
                    context,
                    logger.Object,
                    new EsWmsSieveProcessor(
                        sieveOptions,
                        new SieveCustomFilterMethods(),
                        new SieveCustomSortMethods()));

            await categoryRepository.CreateAsync(category);

            var addedCategory = await context.Set<Category>().FindAsync(category.CategoryId);

            addedCategory.Should().NotBeNull();
            addedCategory.Should().BeEquivalentTo(category);
        }
    }
}
