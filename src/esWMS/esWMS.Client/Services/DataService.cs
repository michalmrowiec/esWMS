using esWMS.Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace esWMS.Client.Services
{
    public interface IDataService<T>
        where T : class
    {
        Task<PagedResultVM<T>> GetPagedResult(string uri, SieveModelVM sieveModel);
        Task<HttpResponseMessage> Create(string uri, T item);

    }
    public class DataService<T>(HttpClient httpClient)
        : IDataService<T>
        where T : class
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<HttpResponseMessage> Create(string uri, T item)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            return response;
        }

        public async Task<PagedResultVM<T>> GetPagedResult(string uri, SieveModelVM sieveModel)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(sieveModel), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<PagedResultVM<T>>(json);

            return responseObj;
        }
    }
}
