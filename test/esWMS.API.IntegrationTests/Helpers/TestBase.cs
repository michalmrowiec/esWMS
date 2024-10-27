using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace esWMS.API.IntegrationTests.Helpers
{
    public class TestBase :
        IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public TestBase(
            CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
