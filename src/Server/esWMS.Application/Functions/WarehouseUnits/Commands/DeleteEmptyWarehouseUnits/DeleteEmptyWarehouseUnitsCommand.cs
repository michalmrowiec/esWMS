using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.DeleteEmptyWarehouseUnits
{
    public record DeleteEmptyWarehouseUnitsCommand : IRequest<BaseResponse>;
}
