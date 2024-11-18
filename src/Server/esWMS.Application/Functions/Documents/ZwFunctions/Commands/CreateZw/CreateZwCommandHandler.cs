using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Services;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetEligibleItemsForZwReturn;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Commands.CreateZw
{
    internal class CreateZwCommandHandler
        (IZwRepository repository,
        IMediator mediator,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<CreateZwCommand, BaseResponse<ZwDto>>
    {
        private readonly IZwRepository _repository = repository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<ZwDto>> Handle
            (CreateZwCommand request, CancellationToken cancellationToken)
        {
            var eligibleItemsForZwReturnResponse = await _mediator.Send(
                new GetEligibleItemsForZwReturnQuery(
                    new SieveModel()
                    {
                        Page = 1,
                        PageSize = 1000,
                        Filters = $"DocumentItemId=={string.Join('|', request.DocumentItemIdQuantity.Select(x => x.DocumentItemId))}"
                    }));

            var eligibleItemsForZwReturn = eligibleItemsForZwReturnResponse.ReturnedObj?.Items ?? [];

            if (!eligibleItemsForZwReturnResponse.IsSuccess() || eligibleItemsForZwReturn.Count == 0)
            {
                return new BaseResponse<ZwDto>(
                    BaseResponse.ResponseStatus.ServerError,
                    "Something went wrong. An error occurred while retrieving the list of document items.");
            }

            var validationResult = await new CreateZwValidator
                (eligibleItemsForZwReturn).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<ZwDto>(validationResult);
            }

            var entity = _mapper.Map<ZW>(request);

            if (entity == null)
            {
                return new BaseResponse<ZwDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var lastNumber = await _repository.GetAllDocumentIdForDay(entity.DocumentIssueDate);

            entity.DocumentId = entity.GenerateDocumentId(lastNumber);
            entity.CreatedAt = DateTime.Now;

            foreach (var item in request.DocumentItemIdQuantity)
            {
                var di = _mapper.Map<DocumentItem>
                    (eligibleItemsForZwReturn.First(x => x.DocumentItemId.Equals(item.DocumentItemId)));
                di.DocumentItemId = Guid.NewGuid().ToString();
                di.DocumentId = entity.DocumentId;
                di.IsApproved = false;
                di.Quantity = item.Quantity;
                di.CreatedAt = DateTime.Now;
                di.CreatedBy = entity.CreatedBy;
                di.ModifiedAt = null;
                di.ModifiedBy = null;
                di.Document = null;
                di.Product = null;

                entity.DocumentItems.Add(di);
            }

            ZwDto entityDto;

            try
            {
                var createdEntity = await _repository.CreateAsync(entity);

                entityDto = _mapper.Map<ZwDto>(createdEntity);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ZwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<ZwDto>(entityDto);
        }
    }
}
