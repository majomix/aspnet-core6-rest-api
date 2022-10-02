using System.Reflection;

namespace DataProcessing.Api
{
    public static class ApiServiceRegistration
    {
        /// <summary>
        /// Registers API services.
        /// </summary>
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
