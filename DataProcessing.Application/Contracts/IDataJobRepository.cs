using DataProcessing.Domain.Entities;

namespace DataProcessing.Application.Contracts
{
    public interface IDataJobRepository : IRepository<DataJob>
    {
        /// <summary>
        /// Gets all items based on their job status.
        /// </summary>
        /// <param name="status">Job status.</param>
        /// <returns>All items with corresponding status.</returns>
        IEnumerable<DataJob> GetDataJobsByStatus(DataJobStatus status);
    }
}
