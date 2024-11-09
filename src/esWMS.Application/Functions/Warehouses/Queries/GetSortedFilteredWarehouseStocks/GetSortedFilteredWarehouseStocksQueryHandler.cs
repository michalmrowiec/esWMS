using AutoMapper;
using esWMS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Warehouses.Queries.GetSortedFilteredWarehouseStocks
{
    internal class GetSortedFilteredWarehouseStocksQueryHandler
        (IWarehouseRepository warehouseRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredWarehouseStocksQuery, BaseResponse<PagedResult<WarehouseStockDto>>>
    {
        private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<WarehouseStockDto>>> Handle
            (GetSortedFilteredWarehouseStocksQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _warehouseRepository.GetWarehouseStocks(request.SieveModel, request.WarehouseId);

           var mapped = _mapper.Map<PagedResult<WarehouseStockDto>>(pagedResult);

            return new BaseResponse<PagedResult<WarehouseStockDto>>(mapped);
        }
    }
}