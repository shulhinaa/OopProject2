using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace lab2.XmlProcessingStrategy
{
    public class XmlLINQProcessing : IXmlProcessing
    {
        public FoundResults Search(Dictionary<string, string> criteria, string xmlContent)
        {
            var results = new FoundResults();
            var doc = XDocument.Parse(xmlContent);

            var matchingElements = doc.Descendants("Workshop")
                .Where(workshop =>
                    criteria.All(criterion =>
                        workshop.Attributes(criterion.Key)
                                .Any(attr => attr.Value.Contains(criterion.Value, StringComparison.OrdinalIgnoreCase))));

            foreach (var element in matchingElements)
            {
                var id = element.Attribute("id")?.Value ?? "N/A";
                results.AddMatch(element.Name.LocalName, id, xmlContent);
            }

            return results;
        }
    }
}


