using esWMS.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace esWMS.API.IntegrationTests.Helpers
{
    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var ef = services.SingleOrDefault(services => services.ServiceType == typeof(InfrastructureInstallation));
                if (ef != null) services.Remove(ef);

                var db = services.SingleOrDefault(services => services.ServiceType == typeof(DbContextOptions<EsWmsDbContext>));
                if (db != null) services.Remove(db);

                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));

                services.AddDbContext<EsWmsDbContext>(options => options.UseInMemoryDatabase(_dbName));

                var options = new DbContextOptionsBuilder<EsWmsDbContext>()
                .UseInMemoryDatabase(databaseName: _dbName)
                .Options;
            });

            builder.UseEnvironment("Development");
        }
    }
}