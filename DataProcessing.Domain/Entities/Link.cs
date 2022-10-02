namespace DataProcessing.Domain.Entities
{
    public class Link
    {
        public string Rel { get; }
        public string Href { get; }
        public string Action { get; }
        public string[] Types { get; }

        public Link(
            string rel,
            string href,
            string action,
            string[] types)
        {
            Rel = rel;
            Href = href;
            Action = action;
            Types = types;
        }
    }
}
