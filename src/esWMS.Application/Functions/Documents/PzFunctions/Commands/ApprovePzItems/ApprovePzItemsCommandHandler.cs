using AutoMapper;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePzItems
{
    internal class ApprovePzItemsCommandHandler
        (IPzRepozitory repository, IMapper mapper, ITransactionManager transactionManager, IMediator mediator)
        : IRequestHandler<ApprovePzItemsCommand, BaseResponse<PzDto>>
    {
        private readonly IPzRepozitory _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;
        private readonly IMediator _mediator = mediator;

        public async Task<BaseResponse<PzDto>> Handle(ApprovePzItemsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new ApprovePzItemsValidator(_mediator).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<PzDto>(validationResult);
            }

            var document = await _repository.GetDocumentByIdWithItemsAsync(request.DocumentId);

            foreach (var item in document.DocumentItems.Where(x => request.DocumentItemsId.Contains(x.DocumentItemId)))
            {
                item.IsApproved = true;
            }

            PzDto mappedUpdatedDocument;

            try
            {
                await _transactionManager.BeginTransactionAsync();

                var updatedDocument = await _repository.UpdateAsync(document);

                await _transactionManager.CommitTransactionAsync();

                mappedUpdatedDocument = _mapper.Map<PzDto>(updatedDocument);
            }
            catch (Exception)
            {
                await _transactionManager.RollbackTransactionAsync();

                return new BaseResponse<PzDto>(false, "Something went wrong.");
            }

            return new BaseResponse<PzDto>(mappedUpdatedDocument);
        }
    }
}
