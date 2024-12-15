using AutoMapper;
using esWMS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;
using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts
{
    internal class GetSortedFilteredProductsQueryHandler
        (IProductRepository productsRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredProductsQuery, BaseResponse<PagedResult<ProductDto>>>
    {
        private readonly IProductRepository _productsRepository = productsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<ProductDto>>> Handle
            (GetSortedFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<ProductDto, Product>(_mapper, _productsRepository);
        }
    }
}