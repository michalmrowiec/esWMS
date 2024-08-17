using esWMS.Client.ViewModels;
using Newtonsoft.Json;
using System.Text;
using static MudBlazor.CategoryTypes;

namespace esWMS.Client.Services
{
    public interface IDataService<T>
        where T : class
    {
        Task<PagedResultVM<T>> GetPagedResult(string uri, SieveModelVM sieveModel, Dictionary<string, string>? queryParams = null);
        Task<HttpResponseMessage> Create(string uri, T item);
    }

    public interface IDocumentDataService
    {
        Task<HttpResponseMessage> ApproveDocument(string uri, object obj);
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

        public async Task<PagedResultVM<T>> GetPagedResult(string uri, SieveModelVM sieveModel, Dictionary<string, string>? queryParams = null)
        {
            if (queryParams != null && queryParams.Count > 0)
            {
                var query = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
                uri = $"{uri}?{query}";
            }

            var postJson = new StringContent(JsonConvert.SerializeObject(sieveModel), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<PagedResultVM<T>>(json);

            return responseObj;
        }
    }

    public class DocumentDataService(HttpClient httpClient)
        : IDocumentDataService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<HttpResponseMessage> ApproveDocument(string uri, object obj)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Patch, uri);
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            return response;
        }
    }
}
