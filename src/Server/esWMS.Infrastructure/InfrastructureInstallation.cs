﻿using esWMS.Domain.Entities.SystemActors;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Infrastructure.Repositories;
using esWMS.Infrastructure.Repositories.Documents;
using esWMS.Infrastructure.Repositories.SystemActors;
using esWMS.Infrastructure.Utilities;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sieve.Services;
using System.Text;
using esWMS.Application.Contracts.Services;
using esWMS.Infrastructure.Services;
using esWMS.Infrastructure.Repositories.WarehouseEnvironment;

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

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            services.AddDbContext<EsWmsDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(configuration.GetConnectionString("ContainerDb"), serverVersion));

            services.AddSingleton(authenticationSettings);
            services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ITransactionManager, EfTransactionManager>();
            services.AddScoped<ISieveCustomFilterMethods, SieveCustomFilterMethods>();
            services.AddScoped<ISieveCustomSortMethods, SieveCustomSortMethods>();
            services.AddScoped<ISieveProcessor, EsWmsSieveProcessor>();

            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseDocumentRepository<>), typeof(BaseDocumentRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IZoneRepository, ZoneRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IWarehouseUnitRepository, WarehouseUnitRepository>();
            services.AddScoped<IWarehouseUnitItemRepository, WarehouseUnitItemRepository>();
            services.AddScoped<IContractorRepository, ContractorRepository>();
            services.AddScoped<IDocumentItemRepository, DocumentItemRepository>();
            services.AddScoped<IPzRepository, PzRepository>();
            services.AddScoped<IWzRepository, WzRepository>();
            services.AddScoped<IMmmRepository, MmmRepository>();
            services.AddScoped<IMmpRepository, MmpRepository>();
            services.AddScoped<IPwRepository, PwRepository>();
            services.AddScoped<IRwRepository, RwRepository>();
            services.AddScoped<IZwRepository, ZwRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
