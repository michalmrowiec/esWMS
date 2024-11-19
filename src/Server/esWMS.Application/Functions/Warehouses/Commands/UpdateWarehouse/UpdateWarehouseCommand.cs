using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Warehouses.Commands.UpdateWarehouse
{
    public class UpdateWarehouseCommand :
        CommonWarehouseCommand,
        IRequest<BaseResponse<WarehouseDto>>
    {
        public string? ModifiedBy { get; set; }
    }
}
