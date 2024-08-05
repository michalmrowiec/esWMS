using esWMS.Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace esWMS.Client.Services
{
    public interface IWarehouseService
    {
        Task<PagedResultVM<WarehouseVM>> GetWarehouse(SieveModelVM sieveModel);
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly HttpClient _httpClient;

        public WarehouseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedResultVM<WarehouseVM>> GetWarehouse(SieveModelVM sieveModel)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(sieveModel), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/warehouse/get-filtered");
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<PagedResultVM<WarehouseVM>>(json);

            return responseObj;
        }
    }
}