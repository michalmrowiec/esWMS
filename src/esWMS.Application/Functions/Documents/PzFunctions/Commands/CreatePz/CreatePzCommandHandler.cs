using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz
{
    internal class CreatePzCommandHandler
        (IPzRepozitory repository, IMapper mapper, ITransactionManager transactionManager)
        : IRequestHandler<CreatePzCommand, BaseResponse<PzDto>>
    {
        private readonly IPzRepozitory _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<PzDto>> Handle(CreatePzCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreatePzValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<PzDto>(validationResult);
            }

            var entity = _mapper.Map<PZ>(request);

            if (entity == null)
            {
                return new BaseResponse<PzDto>(false, "");
            }

            var lastNumber = await _repository.GetAllDocumentIdForDay(entity.DocumentIssueDate);

            entity.DocumentId = entity.GenerateDocumentId(lastNumber);

            foreach (var item in entity.DocumentItems)
            {
                item.DocumentId = entity.DocumentId;
            }

            var createdEntity = await _repository.CreateAsync(entity);

            var entityDto = _mapper.Map<PzDto>(createdEntity);

            return new BaseResponse<PzDto>(entityDto);
        }
    }
}
