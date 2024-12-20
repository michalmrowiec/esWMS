﻿using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface ICategoryRepository
        : IBaseRepository<Category>, ISieve<Category>
    {
        Task<IList<Category>> GetCategoryWithChildren(string idParentCategory);
    }
}
