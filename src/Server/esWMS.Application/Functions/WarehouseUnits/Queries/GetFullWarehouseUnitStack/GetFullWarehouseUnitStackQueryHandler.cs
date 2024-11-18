using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Queries.GetFullWarehouseUnitStack
{
    internal class GetFullWarehouseUnitStackQueryHandler(
        IWarehouseUnitRepository repository,
        IMapper mapper)
        : IRequestHandler<GetFullWarehouseUnitStackQuery, BaseResponse<Dictionary<int, WarehouseUnitDto>>>
    {
        private readonly IWarehouseUnitRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<Dictionary<int, WarehouseUnitDto>>> Handle(
            GetFullWarehouseUnitStackQuery request, CancellationToken cancellationToken)
        {
            Dictionary<int, WarehouseUnitDto> fullStack = new();
            try
            {
                var stack = await _repository.GetFullWarehouseUnitStackAsync(request.WarehouseUnitId);

                var stackDto = _mapper.Map<List<WarehouseUnitDto>>(stack);

                int position = 0;
                foreach (var warehouseUnit in stackDto)
                {
                    fullStack.Add(position, warehouseUnit);
                    position++;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, WarehouseUnitDto>>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<Dictionary<int, WarehouseUnitDto>>(fullStack);
        }
    }
}
