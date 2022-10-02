using DataProcessing.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace DataProcessing.IntegrationTests.AppFactory
{
    internal class TestApplicationFactory : WebApplicationFactory<Program>
    {
        public HttpClient GetClient(TestDataJobRepository repository)
        {
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IDataJobRepository, TestDataJobRepository>(_ => repository);
                });
            }).CreateDefaultClient();
        }
    }
}
