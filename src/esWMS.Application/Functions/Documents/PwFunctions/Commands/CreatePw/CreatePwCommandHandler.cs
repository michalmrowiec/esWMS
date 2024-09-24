using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Services;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Products;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.CreatePw
{
    internal class CreatePwCommandHandler
        (IPwRepository repository,
        IProductService productService,
        IMapper mapper)
        : IRequestHandler<CreatePwCommand, BaseResponse<PwDto>>
    {
        private readonly IPwRepository _repository = repository;
        private readonly IProductService _productService = productService;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PwDto>> Handle
            (CreatePwCommand request, CancellationToken cancellationToken)
        {
            var productsResponse = await _productService.GetProductsAsync(request.DocumentItems.Select(x => x.ProductId));

            if (!productsResponse.IsSuccess)
            {
                return new BaseResponse<PwDto>(
                    BaseResponse.ResponseStatus.ServerError,
                    "Error retrieving products for the document.");
            }

            var validationResult = await new CreatePwValidator(productsResponse.Products)
                .ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<PwDto>(validationResult);
            }

            var entity = await CreatePw(request, productsResponse.Products);

            if (entity == null)
            {
                return new BaseResponse<PwDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return await SavePwEntityAsync(entity);
        }

        private async Task<PW?> CreatePw(CreatePwCommand request, IEnumerable<ProductDto> products)
        {
            var entity = _mapper.Map<PW>(request);

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

        private async Task<BaseResponse<PwDto>> SavePwEntityAsync(PW entity)
        {
            try
            {
                var createdEntity = await _repository.CreateAsync(entity);
                var entityDto = _mapper.Map<PwDto>(createdEntity);

                return new BaseResponse<PwDto>(entityDto);
            }
            catch (Exception)
            {
                return new BaseResponse<PwDto>(BaseResponse.ResponseStatus.ServerError, "Error saving the document.");
            }
        }
    }
}
