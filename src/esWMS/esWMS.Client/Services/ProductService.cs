using esWMS.Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace esWMS.Client.Services
{
    public interface IProductService
    {
        Task<PagedResultVM<ProductVM>> GetProduct(SieveModelVM sieveModel);
    }

    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedResultVM<ProductVM>> GetProduct(SieveModelVM sieveModel)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(sieveModel), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/product/get-filtered");
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<PagedResultVM<ProductVM>>(json);

            return responseObj;
        }
    }
}