using esWMS.API.IntegrationTests.Controllers.Documents.TestData;
using esWMS.API.IntegrationTests.Helpers;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.SystemActors;
using esWMS.Domain.Entities.WarehouseEnvironment;
using esWMS.Domain.Models;
using FluentAssertions;
using Newtonsoft.Json;
using Sieve.Models;
using System.Text;
using Xunit;

namespace esWMS.API.IntegrationTests.Controllers.Documents
{
    public class PzControllerTests(
        CustomWebApplicationFactory<Program> factory)
        : TestBase(factory)
    {
        [Theory]
        [MemberData(
            nameof(GetSortedAndFilteredPzTestData.ValidaData),
            MemberType = typeof(GetSortedAndFilteredPzTestData))]
        public async Task GetSortedAndFilteredPz_ForValidModel_ReturnsSortedAndFilteredPz(
            Warehouse[] startWarehouses,
            Contractor[] startContractors,
            PZ[] startPzDocuments,
            SieveModel sieveModel,
            PagedResult<PzDto> expectedResult)
        {
            var context = GetDbContext().ClearDb();
            await context.CreateDataAsync(startWarehouses);
            await context.CreateDataAsync(startContractors);
            await context.CreateDataAsync(startPzDocuments);

            var json = JsonConvert.SerializeObject(sieveModel);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/pz/get-filtered", httpContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            PagedResult<PzDto>? result =
                JsonConvert.DeserializeObject<PagedResult<PzDto>>(jsonResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().BeEquivalentTo(expectedResult, option => option.WithoutStrictOrdering());
        }
    }
}