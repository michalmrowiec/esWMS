using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Documents.WzFunctions;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz
{
    internal class CreatePzCommandHandler
        (IPzRepository repository,
        IMediator mediator,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<CreatePzCommand, BaseResponse<PzDto>>
    {
        private readonly IPzRepository _repository = repository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<PzDto>> Handle(CreatePzCommand request, CancellationToken cancellationToken)
        {
            var productResponse = await _mediator.Send(
                new GetSortedFilteredProductsQuery(
                    new SieveModel()
                    {
                        Page = 1,
                        PageSize = 500,
                        Filters = "ProductId==" + string.Join('|', request.DocumentItems.Select(x => x.ProductId).Distinct())
                    }));

            var products = productResponse.ReturnedObj?.Items ?? [];

            if (!productResponse.Success)
            {
                return new BaseResponse<PzDto>(false, "Something went wrong.");
            }

            var validationResult = await new CreatePzValidator(products).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<PzDto>(validationResult);
            }

            var entity = _mapper.Map<PZ>(request);

            entity.CreatedAt = DateTime.Now;

            if (entity == null)
            {
                return new BaseResponse<PzDto>(false, "");
            }

            var lastNumber = await _repository.GetAllDocumentIdForDay(entity.DocumentIssueDate);

            entity.DocumentId = entity.GenerateDocumentId(lastNumber);

            foreach (var item in entity.DocumentItems)
            {
                item.DocumentItemId = Guid.NewGuid().ToString();
                item.DocumentId = entity.DocumentId;
                item.IsApproved = false;

                var product = products!.First(x => x.ProductId.Equals(item.ProductId));
                item.ProductCode = product.ProductCode;
                item.EanCode = product.EanCode;
                item.ProductName = product.ProductName;
            }

            PzDto entityDto;

            try
            {
                var createdEntity = await _repository.CreateAsync(entity);

                entityDto = _mapper.Map<PzDto>(createdEntity);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PzDto>(false, "Something went wrong.");
            }

            return new BaseResponse<PzDto>(entityDto);
        }
    }
}
