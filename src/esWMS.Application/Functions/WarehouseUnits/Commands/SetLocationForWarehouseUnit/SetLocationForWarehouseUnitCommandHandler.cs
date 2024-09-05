using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Locations.Queries.GetLocationById;
using esWMS.Application.Functions.WarehouseUnits.Queries.GetWarehouseUnitsByIds;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.SetLocationForWarehouseUnit
{
    internal class SetLocationForWarehouseUnitCommandHandler
        (IWarehouseUnitRepository repository,
        IMapper mapper,
        IMediator mediator)
        : IRequestHandler<SetLocationForWarehouseUnitCommand, BaseResponse<WarehouseUnitDto>>
    {
        private readonly IWarehouseUnitRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<WarehouseUnitDto>> Handle
            (SetLocationForWarehouseUnitCommand request, CancellationToken cancellationToken)
        {
            var warehouseUnitResponse = await _mediator.Send(
                new GetWarehouseUnitsByIdsQuery(request.WarehouseUnitId));

            var newLocationResponse = await _mediator.Send(
                new GetLocationByIdQuery(request.NewLocationId));

            var validationResult =
                await new SetLocationForWarehouseUnitValidator(warehouseUnitResponse, newLocationResponse).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WarehouseUnitDto>(validationResult);
            }

            throw new NotImplementedException();
        }

    }
}
