using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts
{
    internal class GetSortedFilteredProductsQueryHandler
        (IProductRepository productsRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredProductsQuery, BaseResponse<PagedResult<ProductDto>>>
    {
        private readonly IProductRepository _productsRepository = productsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<ProductDto>>> Handle(GetSortedFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _productsRepository.GetSortedFilteredProductsAsync(request.SieveModel);

            var mapped = _mapper.Map<PagedResult<ProductDto>>(pagedResult);

            return new BaseResponse<PagedResult<ProductDto>>(mapped);
        }
    }
}