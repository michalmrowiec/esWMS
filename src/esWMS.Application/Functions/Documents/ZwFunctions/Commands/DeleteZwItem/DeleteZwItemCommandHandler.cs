using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.Documents;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.DeleteZwItem
{
    internal class DeleteZwItemCommandHandler(
        IZwRepository repository,
        IDocumentItemRepository documentItemRepository,
        ITransactionManager transactionManager)
        : IRequestHandler<DeleteZwItemCommand, BaseResponse>
    {
        private readonly IZwRepository _repository = repository;

        private readonly IDocumentItemRepository _documentItemRepository = documentItemRepository;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse> Handle(DeleteZwItemCommand request, CancellationToken cancellationToken)
        {
            DocumentItem documentItem;
            ZW document;
            try
            {
                documentItem = await _documentItemRepository.GetByIdAsync(request.DocumentItemId);

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
