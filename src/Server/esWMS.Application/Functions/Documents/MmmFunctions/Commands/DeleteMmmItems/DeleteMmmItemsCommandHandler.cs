using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.Documents;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Commands.DeleteMmmItems
{
    internal class DeleteMmmItemsCommandHandler(
        IMmmRepository mmmRepository,
        IDocumentItemRepository documentItemRepository,
        IWarehouseUnitRepository warehouseUnitRepository,
        ITransactionManager transactionManager)
        : IRequestHandler<DeleteMmmItemsCommand, BaseResponse>
    {
        private readonly IMmmRepository _mmmRepository = mmmRepository;
        private readonly IDocumentItemRepository _documentItemRepository = documentItemRepository;
        private readonly IWarehouseUnitRepository _warehouseUnitRepository = warehouseUnitRepository;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse> Handle(DeleteMmmItemsCommand request, CancellationToken cancellationToken)
        {
            MMM document;
            IEnumerable<DocumentItem> documentItems;
            try
            {
                document = await _mmmRepository.GetDocumentByIdWithItemsAsync(request.DocumentId);

                documentItems = document.DocumentItems
                            .Where(x => x.DocumentWarehouseUnitItems
                                .Any(x => x.WarehouseUnitItem!.WarehouseUnitId
                                    .Equals(request.WarehouseUnitId)));

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

            if (document.IsApproved)
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentId", $"Cannot delete approved document item.") });

                return new BaseResponse(vr);
            }

            try
            {
                await _transactionManager.BeginTransactionAsync();

                await _warehouseUnitRepository.SetWarehouseUnitsBlockedStatusAsync(false, request.WarehouseUnitId);

                await _documentItemRepository.DeleteRangeAsync(documentItems.Select(x => x.DocumentItemId).ToArray());

                await _transactionManager.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse();
        }
    }
}
