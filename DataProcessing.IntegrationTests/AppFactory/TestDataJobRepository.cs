using DataProcessing.Domain.Entities;
using DataProcessing.Infrastructure.Persistence;

namespace DataProcessing.IntegrationTests.AppFactory
{
    internal class TestDataJobRepository : DataJobRepository
    {
        public void SeedJobs(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var model = new DataJob(Guid.NewGuid(), string.Empty, string.Empty, DataJobStatus.Completed);
                AddOrUpdate(model);
            }
        }
    }
}
