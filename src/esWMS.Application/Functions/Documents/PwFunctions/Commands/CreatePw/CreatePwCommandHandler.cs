using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.CreatePw
{
    internal class CreatePwCommandHandler
        (IPwRepository repository,
        IMediator mediator,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<CreatePwCommand, BaseResponse<PwDto>>
    {
        private readonly IPwRepository _repository = repository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<PwDto>> Handle
            (CreatePwCommand request, CancellationToken cancellationToken)
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

            if (!productResponse.IsSuccess() || products.Count == 0)
            {
                return new BaseResponse<PwDto>(
                    productResponse.Status,
                    "Something went wrong. An error occurred while retrieving the list of products associated with the document.");
            }

            var validationResult = await new CreatePwValidator(products).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<PwDto>(validationResult);
            }

            var entity = _mapper.Map<PW>(request);

            if (entity == null)
            {
                return new BaseResponse<PwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var lastNumber = await _repository.GetAllDocumentIdForDay(entity.DocumentIssueDate);

            entity.DocumentId = entity.GenerateDocumentId(lastNumber);
            entity.CreatedAt = DateTime.Now;

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

            PwDto entityDto;

            try
            {
                var createdEntity = await _repository.CreateAsync(entity);

                entityDto = _mapper.Map<PwDto>(createdEntity);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<PwDto>(entityDto);
        }
    }
}
