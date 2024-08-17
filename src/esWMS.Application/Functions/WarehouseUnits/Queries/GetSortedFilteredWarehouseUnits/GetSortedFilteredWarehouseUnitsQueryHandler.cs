using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Queries.GetSortedFilteredWarehouseUnits
{
    internal class GetSortedFilteredWarehouseUnitsQueryHandler
        (IWarehouseUnitRepository warehouseUnitRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredWarehouseUnitsQuery, BaseResponse<PagedResult<WarehouseUnitDto>>>
    {
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<WarehouseUnitDto>>> Handle(GetSortedFilteredWarehouseUnitsQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _warehouseUnitRepository.GetSortedFilteredAsync(request.SieveModel);

            var mapped = _mapper.Map<PagedResult<WarehouseUnitDto>>(pagedResult);

            return new BaseResponse<PagedResult<WarehouseUnitDto>>(mapped);
        }
    }
}