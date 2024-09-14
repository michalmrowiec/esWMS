using esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts;
using esWMS.Application.Functions.Products;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Services
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductDto> Products, bool IsSuccess)> GetProductsAsync(IEnumerable<string> productIds);
    }

    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<(IEnumerable<ProductDto> Products, bool IsSuccess)> GetProductsAsync(IEnumerable<string> productIds)
        {
            var productResponse = await _mediator.Send(
                new GetSortedFilteredProductsQuery(
                    new SieveModel()
                    {
                        Page = 1,
                        PageSize = 500,
                        Filters = "ProductId==" + string.Join('|', productIds.Distinct())
                    }));

            return new(productResponse.ReturnedObj?.Items ?? [], productResponse.IsSuccess());
        }
    }
}
