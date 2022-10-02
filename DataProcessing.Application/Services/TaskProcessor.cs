using DataProcessing.Application.Contracts;
using DataProcessing.Domain.Entities;

namespace DataProcessing.Application.Services
{
    public class TaskProcessor : ITaskProcessor
    {
        private const int DataProcessingDelay10s = 10000;

        private readonly object _mutex = new();

        private readonly HashSet<Guid> _runningProcessing = new();

        public async Task Process(DataJob job)
        {
            var startTime = DateTime.Now;

            lock (_mutex)
            {
                if (_runningProcessing.Contains(job.Id))
                {
                    return;
                }

                job.Status = DataJobStatus.Processing;
                _runningProcessing.Add(job.Id);
            }

            await Delay();

            lock (_mutex)
            {
                job.AddResults(new List<string>
                {
                    $"Data processing started at {startTime}",
                    $"Data processing finished at {DateTime.Now}"
                });

                job.Status = DataJobStatus.Completed;
                _runningProcessing.Remove(job.Id);
            }
        }

        public bool IsProcessing(DataJob job)
        {
            lock (_mutex)
            {
                return _runningProcessing.Contains(job.Id);
            }
        }

        protected virtual async Task Delay()
        {
            await Task.Delay(DataProcessingDelay10s);
        }
    }
}
