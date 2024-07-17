using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace esWMS.Infrastructure
{
    public static class InfrastructureInstallation
    {
        public static void AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            services.AddDbContext<EsWmsDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(configuration.GetConnectionString("ContainerDb"), serverVersion));
        }
    }
}
