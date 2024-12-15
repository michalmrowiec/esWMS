using esWMS.Application.Functions.WarehouseUnitItems.Commands.DeleteEmptyWarehouseUnitItems;
using esWMS.Application.Functions.WarehouseUnits.Commands.DeleteEmptyWarehouseUnits;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.PurgeEmptyWarehouseUnitsAndItems;

internal class PurgeEmptyWarehouseUnitsAndItemsCommandHandler(
    IMediator mediator)
    : IRequestHandler<PurgeEmptyWarehouseUnitsAndItemsCommand, BaseResponse>
{
    private readonly IMediator _mediator = mediator;

    public async Task<BaseResponse> Handle(
        PurgeEmptyWarehouseUnitsAndItemsCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteEmptyWarehouseUnitItemsCommand(), cancellationToken);
        await _mediator.Send(new DeleteEmptyWarehouseUnitsCommand(), cancellationToken);

        return new BaseResponse();
    }
}
