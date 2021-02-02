using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VirtualMind.Application.Commom.Middlewares;

namespace VirtualMind.Application.Configurations
{
    public static class ServiceCollectionConfiguration
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {            
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());                        
            services.AddMediatR(Assembly.GetExecutingAssembly());                        
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
