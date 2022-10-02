using DataProcessing.Application.Models;
using DataProcessing.Application.Exceptions;

namespace DataProcessing.Application.Contracts
{
    /// <summary>
    /// Entry point into application layer of data processor.
    /// </summary>
    public interface IDataProcessorService
    {
        /// <summary>
        /// Get all persisted data jobs.
        /// </summary>
        /// <returns>Collection of data jobs.</returns>
        IEnumerable<DataJobDto> GetAllDataJobs();

        /// <summary>
        /// Get all persisted data jobs for given status.
        /// </summary>
        /// <returns>Collection of data jobs.</returns>
        IEnumerable<DataJobDto> GetDataJobsByStatus(DataJobStatusDto statusDto);

        /// <summary>
        /// Get a persisted data job by its ID.
        /// </summary>
        /// <returns>Data job.</returns>
        DataJobDto GetDataJob(Guid id);

        /// <summary>
        /// Persist a new data job.
        /// </summary>
        /// <returns>Data job with newly created ID.</returns>
        DataJobDto Create(DataJobDto dataJob);

        /// <summary>
        /// Update a persisted data job.
        /// </summary>
        /// <returns>Snapshot of updated data job.</returns>
        /// <exception cref="NotFoundException">Thrown when item with specified ID is not found.</exception>.
        DataJobDto Update(DataJobDto dataJob);

        /// <summary>
        /// Delete a persisted data job.
        /// </summary>
        /// <exception cref="NotFoundException">Thrown when item with specified ID is not found.</exception>.
        void Delete(Guid dataJobId);

        /// <summary>
        /// Starts background process of previously persisted job.
        /// </summary>
        /// <param name="dataJobId">Job ID.</param>
        /// <returns>True if processing has been started, false otherwise (processing is already ongoing).</returns>
        /// <exception cref="NotFoundException">Thrown when item with specified ID is not found.</exception>.
        bool StartBackgroundProcess(Guid dataJobId);

        /// <summary>
        /// Retrieves current status of job processing.
        /// </summary>
        /// <param name="dataJobId">Job ID.</param>
        /// <returns>Current status.</returns>
        /// <exception cref="NotFoundException">Thrown when item with specified ID is not found.</exception>.
        DataJobStatusDto GetBackgroundProcessStatus(Guid dataJobId);

        /// <summary>
        /// Retrieves result of job processing.
        /// </summary>
        /// <param name="dataJobId">Job ID</param>
        /// <returns>Processing results if processing has been done. Empty collection if it's not yet processed.</returns>
        /// <exception cref="NotFoundException">Thrown when item with specified ID is not found.</exception>.
        List<string> GetBackgroundProcessResults(Guid dataJobId);
    }
}
