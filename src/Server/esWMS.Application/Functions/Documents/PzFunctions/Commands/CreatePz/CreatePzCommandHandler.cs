using AutoMapper;
using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Services;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Products;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;

namespace esWMS.Application.Functions.Documents.PzFunctions.Commands.CreatePz
{
    internal class CreatePzCommandHandler
        (IPzRepository repository,
        IProductService productService,
        IMapper mapper)
        : IRequestHandler<CreatePzCommand, BaseResponse<PzDto>>
    {
        private readonly IPzRepository _repository = repository;
        private readonly IProductService _productService = productService;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PzDto>> Handle
            (CreatePzCommand request, CancellationToken cancellationToken)
        {
            var productsResponse = await _productService.GetProductsAsync(request.DocumentItems.Select(x => x.ProductId));

            if (!productsResponse.IsSuccess)
            {
                return new BaseResponse<PzDto>(
                    BaseResponse.ResponseStatus.ServerError,
                    "Error retrieving products for the document.");
            }

            var validationResult = await new CreatePzValidator(productsResponse.Products)
                .ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<PzDto>(validationResult);
            }

            var entity = await CreatePz(request, productsResponse.Products);

            if (entity == null)
            {
                return new BaseResponse<PzDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return await SavePzEntityAsync(entity);
        }

        private async Task<PZ?> CreatePz(CreatePzCommand request, IEnumerable<ProductDto> products)
        {
            var entity = _mapper.Map<PZ>(request);

            if (entity == null)
                return null;

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

            return entity;
        }

        private async Task<BaseResponse<PzDto>> SavePzEntityAsync(PZ entity)
        {
            try
            {
                var createdEntity = await _repository.CreateAsync(entity);
                var entityDto = _mapper.Map<PzDto>(createdEntity);

                return new BaseResponse<PzDto>(entityDto);
            }
            catch (Exception)
            {
                return new BaseResponse<PzDto>(BaseResponse.ResponseStatus.ServerError, "Error saving the document.");
            }
        }
    }
}
