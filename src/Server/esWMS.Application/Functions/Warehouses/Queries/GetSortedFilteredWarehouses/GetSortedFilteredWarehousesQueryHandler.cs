﻿using AutoMapper;
using esWMS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;
using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Functions.Warehouses.Queries.GetSortedFilteredWarehouses
{
    internal class GetSortedFilteredWarehousesQueryHandler
        (IWarehouseRepository warehouseRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredWarehousesQuery, BaseResponse<PagedResult<WarehouseDto>>>
    {
        private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<WarehouseDto>>> Handle(GetSortedFilteredWarehousesQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<WarehouseDto, Warehouse>(_mapper, _warehouseRepository);

        }
    }
}