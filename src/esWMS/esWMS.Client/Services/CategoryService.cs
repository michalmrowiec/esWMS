using esWMS.Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace esWMS.Client.Services
{
    public interface ICategoryService
    {
        Task<PagedResultVM<CategoryVM>> GetCategory(SieveModelVM sieveModel);
    }

    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedResultVM<CategoryVM>> GetCategory(SieveModelVM sieveModel)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(sieveModel), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/category/get-filtered");
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<BaseServerResponseVM<PagedResultVM<CategoryVM>>>(json);

            return responseObj?.ReturnedObj ?? new();
        }
    }
}