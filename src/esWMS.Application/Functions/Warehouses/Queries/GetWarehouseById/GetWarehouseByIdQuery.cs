using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Warehouses.Queries.GetWarehouseById
{
    public record GetWarehouseByIdQuery(string WarehouseId)
        : IRequest<BaseResponse<WarehouseDto>>;
}
