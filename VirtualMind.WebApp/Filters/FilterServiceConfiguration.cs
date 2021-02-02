using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace VirtualMind.WebApp.Filters
{
    public static class FilterServiceConfiguration
    {
        public static void RegisterGlobalFilters(this IServiceCollection services)
        {
            EnableExceptionFilterAtribute(services);
            SupressDefaultValidators(services);           
        }        

        private static void EnableExceptionFilterAtribute(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
                       options.Filters.Add<ApiExceptionFilterAttribute>())
                           .AddFluentValidation();

        }

        private static void SupressDefaultValidators(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
