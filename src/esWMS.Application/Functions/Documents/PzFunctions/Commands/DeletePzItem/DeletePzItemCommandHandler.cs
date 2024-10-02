using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Products;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.Documents;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.DeletePzItem
{
    internal class DeletePzItemCommandHandler(
        IDocumentItemRepository documentItemRepository,
        ITransactionManager transactionManager)
        : IRequestHandler<DeletePzItemCommand, BaseResponse>
    {
        private readonly IDocumentItemRepository _documentItemRepository = documentItemRepository;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse> Handle(DeletePzItemCommand request, CancellationToken cancellationToken)
        {
            DocumentItem documentItem;

            try
            {
                documentItem = await _documentItemRepository.GetByIdAsync(request.DocumentItemId);
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

            if (documentItem.IsApproved)
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentItemId", $"Cannot delete approved document item.") });

                return new BaseResponse<ProductDto>(vr);
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
                return new BaseResponse<ProductDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse();
        }
    }
}
