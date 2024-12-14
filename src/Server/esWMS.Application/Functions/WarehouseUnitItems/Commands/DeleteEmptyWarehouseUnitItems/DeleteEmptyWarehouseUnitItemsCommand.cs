using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.DeleteEmptyWarehouseUnitItems
{
    public record DeleteEmptyWarehouseUnitItemsCommand : IRequest<BaseResponse>;
}
