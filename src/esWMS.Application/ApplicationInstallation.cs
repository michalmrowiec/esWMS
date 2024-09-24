using esWMS.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace esWMS.Application
{
    public static class ApplicationInstallation
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationInstallation).Assembly);
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
