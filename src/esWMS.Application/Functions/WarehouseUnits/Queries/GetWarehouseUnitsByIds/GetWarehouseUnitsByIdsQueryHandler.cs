using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Queries.GetWarehouseUnitsByIds
{
    internal class GetWarehouseUnitsByIdsQueryHandler(IWarehouseUnitRepository repository, IMapper mapper)
        : IRequestHandler<GetWarehouseUnitsByIdsQuery, BaseResponse<IEnumerable<WarehouseUnitDto>>>
    {
        private readonly IWarehouseUnitRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<IEnumerable<WarehouseUnitDto>>> Handle
            (GetWarehouseUnitsByIdsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetWarehouseUnitsWithItemsByIdAsync(request.WarehouseUniIds);

            var mapped = _mapper.Map<List<WarehouseUnitDto>>(result);

            return new BaseResponse<IEnumerable<WarehouseUnitDto>>(mapped);
        }
    }
}
