using AutoMapper;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.RwFunctions.Commands.ApproveRwItems
{
    internal class ApproveRwItemsCommandHandler
        (IRwRepository rwRepozitory,
        IMapper mapper,
        ITransactionManager transactionManager,
        IMediator mediator)
        : IRequestHandler<ApproveRwItemsCommand, BaseResponse<RwDto>>
    {
        private readonly IRwRepository _rwRepozitory = rwRepozitory;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<RwDto>> Handle(ApproveRwItemsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new ApproveRwItemsValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<RwDto>(validationResult);
            }

            var document = await _rwRepozitory.GetDocumentByIdWithItemsAsync(request.DocumentId);

            foreach (var itemAssignment in request.DocumentItemsWithAssignment)
            {
                var documentItem = document.DocumentItems
                    .First(di => di.DocumentItemId.Equals(itemAssignment.DocumentItemId));

                documentItem.IsApproved = true;
            }

            RwDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedDocument = await _rwRepozitory.UpdateAsync(document);

                await _transactionManager.CommitTransactionAsync();

                mappedUpdatedDocument = _mapper.Map<RwDto>(updatedDocument);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<RwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<RwDto>(mappedUpdatedDocument);
        }
    }
}
