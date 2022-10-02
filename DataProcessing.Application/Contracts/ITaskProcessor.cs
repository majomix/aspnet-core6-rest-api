using DataProcessing.Domain.Entities;

namespace DataProcessing.Application.Contracts
{
    /// <summary>
    /// Thread-safe, single instance task processor.
    /// </summary>
    internal interface ITaskProcessor
    {
        /// <summary>
        /// Starts data processing.
        /// </summary>
        /// <param name="job">Job to process.</param>
        Task Process(DataJob job);
        
        /// <summary>
        /// Checks status of data processing.
        /// </summary>
        /// <param name="job">Job to check.</param>
        /// <returns>True if processing is in progress.</returns>
        bool IsProcessing(DataJob job);
    }
}
