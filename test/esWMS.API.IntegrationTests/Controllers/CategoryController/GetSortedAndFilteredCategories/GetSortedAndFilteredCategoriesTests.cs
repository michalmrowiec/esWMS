using esWMS.API.IntegrationTests.Helpers;
using esWMS.Application.Functions.Categories;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Models;
using FluentAssertions;
using Newtonsoft.Json;
using Sieve.Models;
using System.Text;
using Xunit;

namespace esWMS.API.IntegrationTests.Controllers.CategoryController.GetSortedAndFilteredCategories
{
    public class GetSortedAndFilteredCategoriesTests(
        CustomWebApplicationFactory<Program> factory)
        : TestBase(factory)
    {
        [Theory]
        [MemberData(
            nameof(GetSortedAndFilteredCategoriesTestsData.ValidaData),
            MemberType = typeof(GetSortedAndFilteredCategoriesTestsData))]
        public async Task GetSortedAndFilteredCategories_WithValidModel_ReturnsSortedAndFilteredCategories(
            Category[] startCategories,
            SieveModel sieveModel,
            PagedResult<CategoryDto> expectedResult)
        {
            await GetDbContext().CreateDataAsync(startCategories);

            var json = JsonConvert.SerializeObject(sieveModel);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/v1/category/get-filtered", httpContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            PagedResult<CategoryDto>? result =
                JsonConvert.DeserializeObject<PagedResult<CategoryDto>>(jsonResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
