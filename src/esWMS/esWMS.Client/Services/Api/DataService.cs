using esWMS.Client.Services.Auth;
using esWMS.Client.Services.Notification;
using esWMS.Client.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace esWMS.Client.Services.Api
{
    public interface IDataService
    {
        Task<HttpResponseMessage> Get(string uri, Dictionary<string, string>? queryParams = null);
        Task<HttpResponseMessage> CreateObject(string uri, object item);
        Task<HttpResponseMessage> Patch(string uri, object item);
        Task<HttpResponseMessage> Put(string uri, object item);
        //Task<HttpResponseMessage> Delete(string uri);
        Task<HttpResponseMessage> Delete(string uri, Dictionary<string, string>? queryParams = null);
    }

    public interface IDataService<T> :
        IDataService
        where T : class
    {
        Task<PagedResultVM<T>> GetPagedResult(string uri, SieveModelVM sieveModel, Dictionary<string, string>? queryParams = null);
        Task<HttpResponseMessage> Create(string uri, T item);
    }

    public interface IDocumentDataService
    {
        Task<HttpResponseMessage> ApproveDocument(string uri, object obj);
    }

    public class DataService(
        HttpClient httpClient,
        IAuthService authService)
        : IDataService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IAuthService _authService = authService;

        public async Task<HttpResponseMessage> CreateObject(string uri, object item)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = postJson;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetJwtToken());

            var response = await _httpClient.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(string uri, Dictionary<string, string>? queryParams = null)
        {
            if (queryParams != null && queryParams.Count > 0)
            {
                var query = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
                uri = $"{uri}?{query}";
            }

            using var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetJwtToken());

            var response = await _httpClient.SendAsync(request);

            return response;
        }
        public async Task<HttpResponseMessage> Patch(string uri, object item)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Patch, uri);
            request.Content = postJson;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetJwtToken());

            var response = await _httpClient.SendAsync(request);

            return response;
        }
        public async Task<HttpResponseMessage> Put(string uri, object item)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = postJson;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetJwtToken());

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
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetJwtToken());

            var response = await _httpClient.SendAsync(request);

            return response;
        }
    }
    public class DataService<T>(
        HttpClient httpClient,
        IAuthService authService,
        IAlertService alertService) :
        DataService(httpClient, authService),
        IDataService<T>
        where T : class
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IAuthService _authService = authService;
        private readonly IAlertService _alertService = alertService;

        public async Task<HttpResponseMessage> Create(string uri, T item)
        {
            return await CreateObject(uri, item);
        }



        //public async Task<HttpResponseMessage> Delete(string uri)
        //{
        //    using var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetJwtToken());

        //    var response = await _httpClient.SendAsync(request);

        //    return response;
        //}

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
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetJwtToken());

            var response = await _httpClient.SendAsync(request);

            PagedResultVM<T> responseObj = new();

            if (!response.IsSuccessStatusCode)
            {
                await response.HandleFailure(alertService);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                responseObj = JsonConvert.DeserializeObject<PagedResultVM<T>>(json) ?? new();
            }

            return responseObj;
        }
    }

    public class DocumentDataService(
        HttpClient httpClient,
        IAuthService authService)
        : IDocumentDataService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IAuthService _authService = authService;

        public async Task<HttpResponseMessage> ApproveDocument(string uri, object obj)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = postJson;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetJwtToken());

            var response = await _httpClient.SendAsync(request);

            return response;
        }
    }
}
