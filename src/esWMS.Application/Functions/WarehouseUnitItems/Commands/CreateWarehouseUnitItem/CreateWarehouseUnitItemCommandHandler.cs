using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.CreateWarehouseUnitItem
{
    internal class CreateWarehouseUnitItemCommandHandler
        : IRequestHandler<CreateWarehouseUnitItemCommand, BaseResponse<WarehouseUnitItemDto>>
    {
        private readonly IWarehouseUnitItemRepository _repository;
        private readonly IMapper _mapper;

        public CreateWarehouseUnitItemCommandHandler
            (IWarehouseUnitItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<WarehouseUnitItemDto>> Handle
            (CreateWarehouseUnitItemCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await new CreateWarehouseUnitItemValidator(true).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WarehouseUnitItemDto>(validationResult);
            }

            var entity = _mapper.Map<WarehouseUnitItem>(request);

            entity.WarehouseUnitItemId = Guid.NewGuid().ToString();

            var createdEntity = await _repository.CreateAsync(entity);

            var entityDto = _mapper.Map<WarehouseUnitItemDto>(createdEntity);

            return new BaseResponse<WarehouseUnitItemDto>(entityDto);
        }
    }
}
