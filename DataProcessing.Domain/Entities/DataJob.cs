using DataProcessing.Domain.Contracts;

namespace DataProcessing.Domain.Entities
{
    public class DataJob : IAggregateRoot
    {
        private readonly List<Link> _links = new();
        private readonly List<string> _results = new();

        public Guid Id { get; }
        public string Name { get; }
        public string FilePathToProcess { get; }
        public DataJobStatus Status { get; set; }
        public IEnumerable<string> Results => _results;
        public IEnumerable<Link> Links => _links;

        public DataJob(
            Guid id,
            string name,
            string filePathToProcess,
            DataJobStatus status)
        {
            Id = id;
            Name = name;
            FilePathToProcess = filePathToProcess;
            Status = status;
        }

        public void AddLink(Link link)
        {
            _links.Add(link);
        }

        public void AddResults(List<string> results)
        {
            _results.AddRange(results);
        }
    }
}
