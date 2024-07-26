using esMWS.Domain.Entities.SystemActors;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Utilities;
using esWMS.Infrastructure.Repositories;
using esWMS.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace esWMS.Infrastructure
{
    public static class InfrastructureInstallation
    {
        public static void AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var authenticationSettings = new AuthenticationSettings();
            configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });

            services.AddSingleton(authenticationSettings);

            services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            services.AddDbContext<EsWmsDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(configuration.GetConnectionString("ContainerDb"), serverVersion));

            services.AddScoped<ITransactionManager, EfTransactionManager>();

            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IZoneRepository, ZoneRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IWarehouseUnitRepository, WarehouseUnitRepository>();
            services.AddScoped<IWarehouseUnitItemRepository, WarehouseUnitItemRepository>();
        }
    }
}
