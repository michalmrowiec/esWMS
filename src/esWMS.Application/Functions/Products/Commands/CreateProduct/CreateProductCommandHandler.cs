using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseResponse<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler
            (IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateProductValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<ProductDto>(validationResult);
            }

            var entity = _mapper.Map<Product>(request);

            entity.ProductId = Guid.NewGuid().ToString();

            var createdEntity = await _repository.CreateAsync(entity);

            var entityDto = _mapper.Map<ProductDto>(createdEntity);

            return new BaseResponse<ProductDto>(entityDto);
        }
    }
}
