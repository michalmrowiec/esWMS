using esWMS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface ICategoryRepository
        : IBaseRepository<Category>, ISieve<Category>
    {
        Task<IList<Category>> GetCategoryWithChilds(string idParentCategory);
    }
}
