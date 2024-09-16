using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnitItems.Queries.GetSortedFilteredWarehouseUnitItems
{
    internal class GetSortedFilteredWarehouseUnitItemsQueryHandler
        (IWarehouseUnitItemRepository warehouseUnitItemRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredWarehouseUnitItemsQuery, BaseResponse<PagedResult<WarehouseUnitItemDto>>>
    {
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<WarehouseUnitItemDto>>> Handle
            (GetSortedFilteredWarehouseUnitItemsQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel
                .Handle<WarehouseUnitItemDto, WarehouseUnitItem>(_mapper, _warehouseUnitItemRepository);

        }
    }
}