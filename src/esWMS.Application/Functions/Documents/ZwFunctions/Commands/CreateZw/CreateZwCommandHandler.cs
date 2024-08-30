using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
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
                return new BaseResponse<ZwDto>(
                    productResponse.Status,
                    "Something went wrong. An error occurred while retrieving the list of products associated with the document.");
            }

            var validationResult = await new CreateZwValidator(products).ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<ZwDto>(validationResult);
            }

            var entity = _mapper.Map<ZW>(request);

            if (entity == null)
            {
                return new BaseResponse<ZwDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
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
