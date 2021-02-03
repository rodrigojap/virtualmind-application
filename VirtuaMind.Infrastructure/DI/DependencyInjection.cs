using Microsoft.Extensions.DependencyInjection;
using VirtualMind.Application.Interfaces;
using VirtuaMind.Infrastructure.RestServices.ExternalServices;

namespace VirtuaMind.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IBancoProvinciaRestService, BancoProvinciaService>();

            return services;
        }
    }
}
