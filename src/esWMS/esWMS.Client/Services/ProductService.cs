using esWMS.Client.ViewModels;
using System.Net.Http.Json;

namespace esWMS.Client.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductVM>> GetProduct();
    }

    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductVM>> GetProduct()
        {
            var response = await _httpClient.GetFromJsonAsync<BaseServerResponseVM<PagedResultVM<ProductVM>>>("api/v1/product");

            return response?.ReturnedObj?.Items ?? [];
        }
    }
}