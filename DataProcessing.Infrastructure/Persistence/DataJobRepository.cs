using DataProcessing.Application.Contracts;
using DataProcessing.Domain.Entities;

namespace DataProcessing.Infrastructure.Persistence
{
    internal class DataJobRepository : InMemoryRepository<DataJob>, IDataJobRepository
    {
        public IEnumerable<DataJob> GetDataJobsByStatus(DataJobStatus status)
        {
            return Items.Values.Where(j => j.Status == status);
        }
    }
}
