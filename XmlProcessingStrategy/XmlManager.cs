using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.XmlProcessingStrategy
{
    public class XmlManager
    {
        private IXmlProcessing strategy;

        public XmlManager(IXmlProcessing strategy)
        {
            this.strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public void SetStrategy(IXmlProcessing newStrategy)
        {
            strategy = newStrategy ?? throw new ArgumentNullException(nameof(newStrategy));
        }

        public FoundResults FindResultFromFile(Dictionary<string, string> criteria, string filePath)
        {
            if (criteria == null || !criteria.Any())
                throw new ArgumentException("Search criteria must not be null or empty.");

            string xmlContent = XmlFileHandler.ReadFileAsString(filePath);

            if (string.IsNullOrWhiteSpace(xmlContent))
                throw new ArgumentException("XML file content is empty or invalid.");

            return strategy.Search(criteria, xmlContent);
        }

        public FoundResults FindResultFromString(Dictionary<string, string> criteria, string xmlContent)
        {
            if (criteria == null || !criteria.Any())
                throw new ArgumentException("Search criteria must not be null or empty.");

            if (string.IsNullOrWhiteSpace(xmlContent))
                throw new ArgumentException("XML content must not be null or empty.");

            return strategy.Search(criteria, xmlContent);
        }
    }
}

