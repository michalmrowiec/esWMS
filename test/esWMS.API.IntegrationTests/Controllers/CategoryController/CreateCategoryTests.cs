using esWMS.API.IntegrationTests.Helpers;
using esWMS.Application.Functions.Categories.Commands.CreateCategory;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace esWMS.API.IntegrationTests.Controllers.CategoryController
{
    public class CreateCategoryTests(
        CustomWebApplicationFactory<Program> factory)
        : TestBase(factory)
    {
        public static IEnumerable<object[]> TestCategories => new List<object[]>
        {
            new object[] { new CreateCategoryCommand { CategoryName = "test" }},
            new object[] { new CreateCategoryCommand { CategoryName = "twenty five character 123" }},
            new object[] { new CreateCategoryCommand { CategoryName = "!@#$%^&*()_+TEST" }},
            new object[] { new CreateCategoryCommand { CategoryName = "TE ST" }},
            new object[] { new CreateCategoryCommand { CategoryName = "90" }}
        };

        [Theory]
        [MemberData(nameof(TestCategories))]
        public async Task CreateCategory_WithValidModel_ReturnsCreatedStatus(
            CreateCategoryCommand category)
        {
            var json = JsonConvert.SerializeObject(category);
            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/v1/category", httpContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
    }
}
