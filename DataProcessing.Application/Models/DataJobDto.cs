namespace DataProcessing.Application.Models
{
    public class DataJobDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePathToProcess { get; set; }
        public DataJobStatusDto Status { get; set; } = DataJobStatusDto.New;
        public IEnumerable<string> Results { get; set; } = new List<string>();
        public IEnumerable<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
