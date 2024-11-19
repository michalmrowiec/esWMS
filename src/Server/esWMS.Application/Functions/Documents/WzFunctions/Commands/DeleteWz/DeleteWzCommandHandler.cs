using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.Documents;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.DeleteWz
{
    internal class DeleteWzCommandHandler(
        IWzRepository repository,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        ITransactionManager transactionManager)
        : IRequestHandler<DeleteWzCommand, BaseResponse>
    {
        private readonly IWzRepository _repository = repository;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse> Handle(DeleteWzCommand request, CancellationToken cancellationToken)
        {
            WZ document;

            try
            {
                document = await _repository.GetDocumentByIdWithItemsAsync(request.DocumentId);
            }
            catch (KeyNotFoundException)
            {
                return new BaseResponse
                    (BaseResponse.ResponseStatus.NotFound, "Document not found.");
            }
            catch (Exception)
            {
                return new BaseResponse
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            if (document.IsApproved
                || document.DocumentItems.Any(x => x.IsApproved))
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                new("DocumentId", $"An approved document cannot be deleted or the document contains approved items.") });

                return new BaseResponse(vr);
            }


            foreach (var dwui in document.DocumentItems.SelectMany(x => x.DocumentWarehouseUnitItems))
            {
                dwui.WarehouseUnitItem!.BlockedQuantity -= dwui.Quantity;
            }

            try
            {
                await _transactionManager.BeginTransactionAsync();

                await _warehouseUnitItemRepository.UpdateWarehouseUnitItemsAsync(
                    document.DocumentItems
                        .SelectMany(x => x.DocumentWarehouseUnitItems)
                        .Select(x => x.WarehouseUnitItem!)
                        .ToArray());

                await _repository.DeleteAsync(request.DocumentId);

                await _transactionManager.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();
                return new BaseResponse
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse();
        }
    }
}
