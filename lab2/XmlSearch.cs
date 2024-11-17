using lab2.XmlProcessingStrategy;


namespace lab2
{
    public class SearchCommandService
    {
        private readonly IXmlProcessing strategy;

        public SearchCommandService(IXmlProcessing strategy)
        {
            this.strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public FoundResults ExecuteSearch(Dictionary<string, string> criteria, string xmlContent)
        {
            if (criteria == null || !criteria.Any())
                throw new ArgumentException("Search criteria cannot be null or empty.", nameof(criteria));

            if (string.IsNullOrWhiteSpace(xmlContent))
                throw new ArgumentException("XML content cannot be null or empty.", nameof(xmlContent));

            
            return strategy.Search(criteria, xmlContent);
        }
    }
}

