namespace DataProcessing.Application.Models
{
    public class LinkDto
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Action { get; set; }
        public string[] Types { get; set; }
    }
}
