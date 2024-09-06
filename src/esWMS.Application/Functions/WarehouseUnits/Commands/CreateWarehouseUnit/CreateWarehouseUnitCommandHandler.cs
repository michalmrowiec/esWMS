using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Locations.Queries.GetLocationById;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.CreateWarehouseUnit
{
    internal class CreateWarehouseUnitCommandHandler
        (IWarehouseUnitRepository repository,
        IMediator mediator,
        IMapper mapper)
        : IRequestHandler<CreateWarehouseUnitCommand, BaseResponse<WarehouseUnitDto>>
    {
        private readonly IWarehouseUnitRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<WarehouseUnitDto>> Handle
            (CreateWarehouseUnitCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await new CreateWarehouseUnitValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WarehouseUnitDto>(validationResult);
            }

            var warehouseUnitEntity = _mapper.Map<WarehouseUnit>(request);

            warehouseUnitEntity.WarehouseUnitId = Guid.NewGuid().ToString();

            foreach (var item in request.WarehouseUnitItems)
            {
                var warehouseUnitItemEntity = _mapper.Map<WarehouseUnitItem>(item);

                warehouseUnitItemEntity.WarehouseUnitItemId = Guid.NewGuid().ToString();
                warehouseUnitItemEntity.WarehouseUnitId = warehouseUnitEntity.WarehouseUnitId;

                warehouseUnitEntity.WarehouseUnitItems.Add(warehouseUnitItemEntity);
            }

            var createdEntity = await _repository.CreateAsync(warehouseUnitEntity);

            var entityDto = _mapper.Map<WarehouseUnitDto>(createdEntity);

            return new BaseResponse<WarehouseUnitDto>(entityDto);
        }
    }
}
