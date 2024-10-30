using esWMS.Application.Contracts.Persistence;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.WarehouseEnviroment
{
    public class CategoryRepository(EsWmsDbContext context, ILogger<CategoryRepository> logger, ISieveProcessor sieveProcessor)
        : BaseRepository<Category>(context, logger), ICategoryRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public Task<IList<Category>> GetCategoryWithChilds(string idParentCategory)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<Category>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var categories = _context.Categories
                .Include(p => p.ChildCategories)
                .Include(p => p.ParentCategory)
                .AsNoTracking()
                .AsQueryable();

            var filteredCategories = await _sieveProcessor
                .Apply(sieveModel, categories)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, categories, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<Category>(filteredCategories, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
