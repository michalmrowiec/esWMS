using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Products.Queries.GetProductById
{
    internal class GetProductByIdQueryHandler
        (IProductRepository repository,
        IMapper mapper)
        : IRequestHandler<GetProductByIdQuery, BaseResponse<ProductDto>>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<ProductDto>> Handle
            (GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ProductId))
                return new BaseResponse<ProductDto>
                    (BaseResponse.ResponseStatus.BadQuery, "No product unit IDs provided.");

            var result = await _repository.GetByIdAsync(request.ProductId);

            var mapped = _mapper.Map<ProductDto>(result);

            return new BaseResponse<ProductDto>(mapped);
        }
    }
}
