using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Warehouses.Commands.CreateWarehouse
{
    public class CreateWarehouseCommand :
        CommonWarehouseCommand,
        IRequest<BaseResponse<WarehouseDto>>
    {
        public string? CreatedBy { get; set; }
    }
}
