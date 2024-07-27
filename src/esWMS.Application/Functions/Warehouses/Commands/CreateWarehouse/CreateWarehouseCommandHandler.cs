using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Warehouses.Commands.CreateWarehouse
{
    internal class CreateWarehouseCommandHandler
        : IRequestHandler<CreateWarehouseCommand, BaseResponse<WarehouseDto>>
    {
        private readonly IWarehouseRepository _repository;
        private readonly IMapper _mapper;

        public CreateWarehouseCommandHandler
            (IWarehouseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<WarehouseDto>> Handle
            (CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await new CreateWarehouseValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WarehouseDto>(validationResult);
            }

            var entity = _mapper.Map<Warehouse>(request);

            var createdEntity = await _repository.CreateAsync(entity);

            var entityDto = _mapper.Map<WarehouseDto>(createdEntity);

            return new BaseResponse<WarehouseDto>(entityDto);
        }
    }
}
