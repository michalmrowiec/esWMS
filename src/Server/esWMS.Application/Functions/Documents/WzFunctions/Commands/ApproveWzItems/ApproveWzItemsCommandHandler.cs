using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Commands.ApproveWzItems
{
    internal class ApproveWzItemsCommandHandler
        (IWzRepository wzRepozitory,
        IMapper mapper,
        ITransactionManager transactionManager,
        IMediator mediator)
        : IRequestHandler<ApproveWzItemsCommand, BaseResponse<WzDto>>
    {
        private readonly IWzRepository _wzRepozitory = wzRepozitory;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<WzDto>> Handle(ApproveWzItemsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new ApproveWzItemsValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<WzDto>(validationResult);
            }

            var document = await _wzRepozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);

            foreach (var itemAssignment in request.DocumentItemsWithAssignment)
            {
                var documentItem = document.DocumentItems
                    .First(di => di.DocumentItemId.Equals(itemAssignment.DocumentItemId));

                documentItem.IsApproved = true;
            }

            WzDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedDocument = await _wzRepozitory.UpdateAsync(document);

                await _transactionManager.CommitTransactionAsync();

                mappedUpdatedDocument = _mapper.Map<WzDto>(updatedDocument);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<WzDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<WzDto>(mappedUpdatedDocument);
        }
    }
}
