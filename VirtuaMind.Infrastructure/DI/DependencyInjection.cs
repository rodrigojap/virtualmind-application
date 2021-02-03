using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VirtualMind.Application.Interfaces;
using VirtuaMind.Infrastructure.Persistence;
using VirtuaMind.Infrastructure.RestServices.ExternalServices;

namespace VirtuaMind.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            AddDatabase(services);
            AddRestServicesDependency(services);

            return services;
        }

        private static void AddDatabase(IServiceCollection services)
        {
            services.AddDbContext<VirtualMindDbContext>(options =>
                                options.UseInMemoryDatabase("VirtualMindDB"));

            services.AddScoped<IVirtualMindDbContext>(provider => provider.GetService<VirtualMindDbContext>());
        }

        private static void AddRestServicesDependency(IServiceCollection services)
        {
            services.AddScoped<IBancoProvinciaRestService, BancoProvinciaService>();
        }
    }
}
