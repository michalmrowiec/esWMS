using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.Documents;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.DeleteWzItem
{
    internal class DeleteWzItemCommandHandler(
        IWzRepository wzRepository,
        IDocumentItemRepository documentItemRepository,
        IWarehouseUnitItemRepository warehouseUnitItemRepository,
        ITransactionManager transactionManager)
        : IRequestHandler<DeleteWzItemCommand, BaseResponse>
    {
        private readonly IWzRepository _wzRepository = wzRepository;
        private readonly IDocumentItemRepository _documentItemRepository = documentItemRepository;
        private readonly ITransactionManager _transactionManager = transactionManager;
        private readonly IWarehouseUnitItemRepository _warehouseUnitItemRepository = warehouseUnitItemRepository;

        public async Task<BaseResponse> Handle(DeleteWzItemCommand request, CancellationToken cancellationToken)
        {
            DocumentItem documentItem;
            WZ document;
            List<DocumentWarehouseUnitItem> documentWarehouseUnitItems;

            try
            {
                documentItem = await _documentItemRepository
                    .GetDocumentItemByIdWithAssignments(request.DocumentItemId);

                document = await _wzRepository.GetByIdAsync(documentItem.DocumentId);

                documentWarehouseUnitItems = documentItem.DocumentWarehouseUnitItems.ToList();
            }
            catch (KeyNotFoundException)
            {
                return new BaseResponse
                    (BaseResponse.ResponseStatus.NotFound, "Document item not found.");
            }
            catch (Exception)
            {
                return new BaseResponse
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            if (!document.DocumentItems.Contains(documentItem))
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentItemId", $"Incorrect document item.") });

                return new BaseResponse(vr);
            }

            if (documentItem.IsApproved)
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentItemId", $"Cannot delete approved document item.") });

                return new BaseResponse(vr);
            }

            foreach (var dwui in documentWarehouseUnitItems)
            {
                dwui.WarehouseUnitItem!.BlockedQuantity -= dwui.Quantity;
            }

            try
            {
                await _transactionManager.BeginTransactionAsync();

                await _warehouseUnitItemRepository.UpdateWarehouseUnitItemsAsync(
                    documentWarehouseUnitItems.Select(x => x.WarehouseUnitItem!).ToArray());

                await _documentItemRepository.DeleteAsync(request.DocumentItemId);

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
