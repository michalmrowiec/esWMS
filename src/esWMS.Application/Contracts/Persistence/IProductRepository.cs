﻿using esWMS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IProductRepository
        : IBaseRepository<Product>, ISieve<Product>
    {
    }
}
