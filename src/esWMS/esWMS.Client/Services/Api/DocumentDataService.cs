using esWMS.Client.Services.Auth;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace esWMS.Client.Services.Api
{
    public interface IDocumentDataService
    {
        Task<HttpResponseMessage> ApproveDocument(string uri, object obj);
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
