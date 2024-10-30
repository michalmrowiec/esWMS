using esWMS.API.IntegrationTests.Helpers;
using esWMS.Application.Functions.Categories.Commands.CreateCategory;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace esWMS.API.IntegrationTests.Controllers.CategoryController.CreateCategory
{
    public class CreateCategoryTests(
        CustomWebApplicationFactory<Program> factory)
        : TestBase(factory)
    {
        [Theory]
        [MemberData(
            nameof(CreateCategoryTestsData.ValidaData),
            MemberType = typeof(CreateCategoryTestsData))]
        public async Task CreateCategory_WithValidModel_ReturnsCreatedStatus(
            CreateCategoryCommand category)
        {
            var json = JsonConvert.SerializeObject(category);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/v1/category", httpContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
    }
}
