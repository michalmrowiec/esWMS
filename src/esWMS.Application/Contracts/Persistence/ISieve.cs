﻿using esWMS.Domain.Models;
using Sieve.Models;

namespace esWMS.Application.Contracts.Persistence
{
    public interface ISieve<TEntity>
        where TEntity : class
    {
        Task<PagedResult<TEntity>> GetSortedFilteredAsync(SieveModel sieveModel);
    }
}
