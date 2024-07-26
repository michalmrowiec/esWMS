using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class CategoryRepository(EsWmsDbContext context, ILogger<CategoryRepository> logger)
        : BaseRepository<Category>(context, logger), ICategoryRepository
    {

        public Task<IList<Category>> GetCategoryWithChilds(string idParentCategory)
        {
            throw new NotImplementedException();
        }
    }
}
