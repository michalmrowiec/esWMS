using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;
using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.DeleteWarehouseUnit
{
    internal class DeleteWarehouseUnitCommandHandler
        (IWarehouseUnitRepository repository)
        : IRequestHandler<DeleteWarehouseUnitCommand, BaseResponse>
    {
        private readonly IWarehouseUnitRepository _repository = repository;

        public async Task<BaseResponse> Handle(DeleteWarehouseUnitCommand request, CancellationToken cancellationToken)
        {
            WarehouseUnit? warehouseUnit;
            try
            {
                var warehouseUnits = await _repository.GetWarehouseUnitsWithItemsByIdsAsync(request.WarehouseUnitId);
                warehouseUnit = warehouseUnits.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong");
            }

            if (warehouseUnit == null)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.NotFound, "Warehouse unit not found");
            }

            var validationResult = await new DeleteWarehouseUnitValidator
                (warehouseUnit).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse(validationResult);
            }

            try
            {
                await _repository.DeleteAsync(request.WarehouseUnitId);
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong");
            }

            return new BaseResponse();
        }
    }
}
