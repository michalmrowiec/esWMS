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
            if (request.WarehouseUniIds == null || !request.WarehouseUniIds.Any())
                return new BaseResponse<IEnumerable<WarehouseUnitDto>>
                    (BaseResponse.ResponseStatus.BadQuery, "No warehouse unit IDs provided.");

            var result = await _repository.GetWarehouseUnitsWithItemsByIdsAsync(request.WarehouseUniIds);

            var mapped = _mapper.Map<List<WarehouseUnitDto>>(result);

            return new BaseResponse<IEnumerable<WarehouseUnitDto>>(mapped);
        }
    }
}
