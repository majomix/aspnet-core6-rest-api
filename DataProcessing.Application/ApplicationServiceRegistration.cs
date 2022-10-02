using DataProcessing.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using DataProcessing.Application.Services;
using System.Reflection;

namespace DataProcessing.Application
{
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// Registers application services.
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IDataProcessorService, DataProcessorService>();
            services.AddSingleton<ITaskProcessor, TaskProcessor>();

            return services;
        }
    }
}
