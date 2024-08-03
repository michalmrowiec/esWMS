using AutoMapper;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using FluentValidation.Results;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.ApprovePz
{
    internal class ApprovePzCommandHanlder
        (IPzRepository repository, IMapper mapper, ITransactionManager transactionManager)
        : IRequestHandler<ApprovePzCommand, BaseResponse<PzDto>>
    {
        private readonly IPzRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<PzDto>> Handle(ApprovePzCommand request, CancellationToken cancellationToken)
        {
            var document = await _repository.GetDocumentByIdWithItemsAsync(request.DocumentId);

            if (document == null)
            {
                var vr = new ValidationResult(
                    new List<ValidationFailure>() {
                        new("DocumentId", $"The document by Id: {request.DocumentId} does not exist.") });

                return new BaseResponse<PzDto>(vr);
            }

            document.ApprovingEmployeeId = request.ModifiedBy;
            document.IsApproved = true;
            document.AprovedDate = DateTime.Now;
            document.ModifiedAt = DateTime.Now;
            document.ModifiedBy = request.ModifiedBy;

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
