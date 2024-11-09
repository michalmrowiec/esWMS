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
        Task<HttpResponseMessage> Delete(string uri, Dictionary<string, string>? queryParams = null);
    }

    public interface IDataService<T> :
        IDataService
        where T : class
    {
        Task<PagedResultVM<T>> GetPagedResult(string uri, SieveModelVM sieveModel, Dictionary<string, string>? queryParams = null);
        Task<HttpResponseMessage> Create(string uri, T item);
    }

    public class DataService(
        HttpClient httpClient,
        IAuthService authService)
        : IDataService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IAuthService _authService = authService;

        protected async Task<HttpResponseMessage> BaseRequestWithAuth(
            HttpMethod httpMethod,
            string uri,
            object? item = null,
            Dictionary<string, string>? queryParams = null)
        {
            if (queryParams != null && queryParams.Count > 0)
            {
                var query = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
                uri = $"{uri}?{query}";
            }

            using var request = new HttpRequestMessage(httpMethod, uri);

            if (item != null)
            {
                var postJson = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                request.Content = postJson;
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _authService.GetJwtToken());
            var response = await _httpClient.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> CreateObject(string uri, object item)
        {
            return await BaseRequestWithAuth(
                HttpMethod.Post,
                uri: uri,
                item: item);
        }

        public async Task<HttpResponseMessage> Delete(string uri, Dictionary<string, string>? queryParams = null)
        {
            return await BaseRequestWithAuth(
                HttpMethod.Delete,
                uri: uri,
                queryParams: queryParams);

        }

        public async Task<HttpResponseMessage> Patch(string uri, object item)
        {
            return await BaseRequestWithAuth(
                HttpMethod.Patch,
                uri: uri,
                item: item);
        }

        public async Task<HttpResponseMessage> Put(string uri, object item)
        {
            return await BaseRequestWithAuth(
                HttpMethod.Put,
                uri: uri,
                item: item);
        }

        public async Task<HttpResponseMessage> Get(string uri, Dictionary<string, string>? queryParams = null)
        {
            return await BaseRequestWithAuth(
                HttpMethod.Get,
                uri: uri,
                queryParams: queryParams);
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

        public async Task<PagedResultVM<T>> GetPagedResult(string uri, SieveModelVM sieveModel, Dictionary<string, string>? queryParams = null)
        {
            var response = await BaseRequestWithAuth(
                HttpMethod.Post,
                uri: uri,
                item: sieveModel,
                queryParams: queryParams);

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
}
