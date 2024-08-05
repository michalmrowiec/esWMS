using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using esWMS.Application.Responses;
using MediatR;

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
            var pagedResult = await _warehouseRepository.GetSortedFilteredAsync(request.SieveModel);

            var mapped = _mapper.Map<PagedResult<WarehouseDto>>(pagedResult);

            return new BaseResponse<PagedResult<WarehouseDto>>(mapped);
        }
    }
}