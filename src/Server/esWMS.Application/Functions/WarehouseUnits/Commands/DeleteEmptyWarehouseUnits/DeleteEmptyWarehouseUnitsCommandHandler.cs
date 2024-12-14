using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnits.Commands.DeleteEmptyWarehouseUnits
{
    internal class DeleteEmptyWarehouseUnitCommandHandler(
        IWarehouseUnitRepository repository)
        : IRequestHandler<DeleteEmptyWarehouseUnitsCommand, BaseResponse>
    {
        private readonly IWarehouseUnitRepository _repository = repository;

        public async Task<BaseResponse> Handle(DeleteEmptyWarehouseUnitsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteEmptyWarehouseUnits();
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong");
            }

            return new BaseResponse();
        }
    }
}
