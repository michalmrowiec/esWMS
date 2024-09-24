using esWMS.Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace esWMS.Client.Services
{
    public interface IDataService<T>
        where T : class
    {
        Task<PagedResultVM<T>> GetPagedResult(string uri, SieveModelVM sieveModel, Dictionary<string, string>? queryParams = null);
        Task<HttpResponseMessage> Get(string uri, Dictionary<string, string>? queryParams = null);
        Task<HttpResponseMessage> Create(string uri, T item);
        Task<HttpResponseMessage> CreateObject(string uri, object item);
        Task<HttpResponseMessage> Patch(string uri, object item);
        Task<HttpResponseMessage> Delete(string uri);
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
            return await CreateObject(uri, item);
        }

        public async Task<HttpResponseMessage> CreateObject(string uri, object item)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(string uri)
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            var response = await _httpClient.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> Get(string uri, Dictionary<string, string>? queryParams = null)
        {
            if (queryParams != null && queryParams.Count > 0)
            {
                var query = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
                uri = $"{uri}?{query}";
            }

            using var request = new HttpRequestMessage(HttpMethod.Get, uri);

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

        public async Task<HttpResponseMessage> Patch(string uri, object item)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Patch, uri);
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            return response;
        }
    }

    public class DocumentDataService(HttpClient httpClient)
        : IDocumentDataService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<HttpResponseMessage> ApproveDocument(string uri, object obj)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = postJson;

            var response = await _httpClient.SendAsync(request);

            return response;
        }
    }
}
