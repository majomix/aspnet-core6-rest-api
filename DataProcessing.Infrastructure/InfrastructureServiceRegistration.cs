using DataProcessing.Application.Contracts;
using DataProcessing.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace DataProcessing.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        /// <summary>
        /// Registers infrastructure services.
        /// </summary>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IDataJobRepository, DataJobRepository>();

            return services;
        }
    }
}
