using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.WarehouseUnitItems.Commands.DeleteEmptyWarehouseUnitItems
{
    internal class DeleteEmptyWarehouseUnitItemsCommandHandler(
        IWarehouseUnitItemRepository repository)
        : IRequestHandler<DeleteEmptyWarehouseUnitItemsCommand, BaseResponse>
    {
        private readonly IWarehouseUnitItemRepository _repository = repository;

        public async Task<BaseResponse> Handle(DeleteEmptyWarehouseUnitItemsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteEmptyWarehouseUnitItems();
            }
            catch (Exception ex)
            {
                return new BaseResponse(BaseResponse.ResponseStatus.ServerError, "Something went wrong");
            }

            return new BaseResponse();
        }
    }
}
