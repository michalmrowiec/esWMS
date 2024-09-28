using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Products.Commands.CreateProduct;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Products.Commands.UpdateProduct
{
    internal class UpdateProductCommandHandler
        (IProductRepository repository,
        IMapper mapper)
        : IRequestHandler<UpdateProductCommand, BaseResponse<ProductDto>>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<ProductDto>> Handle
            (UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateProductValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<ProductDto>(validationResult);
            }

            try
            {
                var existingEntity = await _repository.GetByIdAsync(request.ProductId);

                var mapped = _mapper.Map(request, existingEntity);

                var updated = await _repository.UpdateAsync(mapped);

                var mappedDto = _mapper.Map<ProductDto>(updated);

                return new BaseResponse<ProductDto>(mappedDto);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse<ProductDto>
                    (BaseResponse.ResponseStatus.NotFound, "Product not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }
        }
    }
}
