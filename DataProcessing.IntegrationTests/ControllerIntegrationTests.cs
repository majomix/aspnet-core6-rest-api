using DataProcessing.Application.Models;
using DataProcessing.IntegrationTests.AppFactory;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DataProcessing.IntegrationTests
{
    [TestFixture]
    public class ControllerIntegrationTests
    {
        [Test]
        public async Task GetDataJob_ReturnsMockedData()
        {
            // arrange
            var jobsCount = 5;
            var endPoint = "/api/DataJob";

            var testRepo = new TestDataJobRepository();
            testRepo.SeedJobs(jobsCount);

            var factory = new TestApplicationFactory();
            var client = factory.GetClient(testRepo);
            
            // act
            var response = await client.GetAsync(endPoint);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<DataJobDto>>(responseString);

            // assert
            response.EnsureSuccessStatusCode();
            result.Count.Should().Be(jobsCount);
        }
    }
}
