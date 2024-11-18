using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.Documents;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.DeletePwItem
{
    internal class DeletePwItemCommandHandler(
        IPwRepository repository,
        IDocumentItemRepository documentItemRepository,
        ITransactionManager transactionManager)
        : IRequestHandler<DeletePwItemCommand, BaseResponse>
    {
        private readonly IPwRepository _repository = repository;
        private readonly IDocumentItemRepository _documentItemRepository = documentItemRepository;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse> Handle(DeletePwItemCommand request, CancellationToken cancellationToken)
        {
            DocumentItem documentItem;
            PW document;
            try
            {
                documentItem = await _documentItemRepository.GetDocumentItemByIdWithAssignments(request.DocumentItemId);
                document = await _repository.GetByIdAsync(documentItem.DocumentId);
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

            if (documentItem.DocumentWarehouseUnitItems.Any())
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentItemId", $"Cannot delete an assigned or partially assigned item.") });
                return new BaseResponse(vr);
            }

            try
            {
                await _transactionManager.BeginTransactionAsync();

                await _documentItemRepository.DeleteAsync(request.DocumentItemId);

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
