using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Warehouses.Queries.GetWarehouseById
{
    internal class GetWarehouseByIdQueryHandler(
        IWarehouseRepository repository,
        IMapper mapper) :
        IRequestHandler<GetWarehouseByIdQuery, BaseResponse<WarehouseDto>>
    {
        private readonly IWarehouseRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<WarehouseDto>> Handle(
            GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.WarehouseId))
                return new BaseResponse<WarehouseDto>
                    (BaseResponse.ResponseStatus.BadQuery, "No warehouse IDs provided.");

            var result = await _repository.GetByIdAsync(request.WarehouseId);

            var mapped = _mapper.Map<WarehouseDto>(result);

            return new BaseResponse<WarehouseDto>(mapped);
        }
    }
}
