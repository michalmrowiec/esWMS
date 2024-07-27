using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface ICategoryRepository
        : IBaseRepository<Category>
    {
        Task<IList<Category>> GetCategoryWithChilds(string idParentCategory);
    }
}
