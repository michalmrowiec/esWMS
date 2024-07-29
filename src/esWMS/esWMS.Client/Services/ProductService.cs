using esWMS.Client.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace esWMS.Client.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductVM>> GetProduct();
        Task<PagedResultVM<ProductVM>> GetProduct2(SieveModelVM sieveModel);
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

        public async Task<PagedResultVM<ProductVM>> GetProduct2(SieveModelVM sieveModel)
        {
            //var response = await _httpClient.GetFromJsonAsync<BaseServerResponseVM<PagedResultVM<ProductVM>>>("api/v1/product");

            //var response = await _httpClient.GetAsync("api/v1/product");
            var postJson = new StringContent(JsonConvert.SerializeObject(sieveModel), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/product/get-filtered");
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<BaseServerResponseVM<PagedResultVM<ProductVM>>>(json);

            return responseObj?.ReturnedObj ?? new();
        }
    }
}