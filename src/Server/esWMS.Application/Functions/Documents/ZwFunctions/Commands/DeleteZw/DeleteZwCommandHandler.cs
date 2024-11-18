using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.Documents;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.DeleteZw
{
    internal class DeleteZwCommandHandler(
        IZwRepository repository,
        ITransactionManager transactionManager)
        : IRequestHandler<DeleteZwCommand, BaseResponse>
    {
        private readonly IZwRepository _repository = repository;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse> Handle(DeleteZwCommand request, CancellationToken cancellationToken)
        {
            ZW document;

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

            if (document.DocumentItems.Any(x => x.DocumentWarehouseUnitItems.Any()))
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentItemId", $"Cannot delete a document with an assigned or partially assigned item.") });
                return new BaseResponse(vr);
            }

            try
            {
                await _transactionManager.BeginTransactionAsync();

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
