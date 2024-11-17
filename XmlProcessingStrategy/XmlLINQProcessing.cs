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
                        MatchesCriterion(workshop, criterion.Key, criterion.Value)));

            foreach (var workshop in matchingElements)
            {
                var attributes = GetAllWorkshopAttributes(workshop);

                results.AddMatch(workshop.Name.LocalName, attributes["id"], xmlContent);

            }

            return results;
        }

        private bool MatchesCriterion(XElement element, string key, string value)
        {
            var attr = element.Attribute(key);
            if (attr != null && attr.Value.Contains(value, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            foreach (var session in element.Elements("Session"))
            {
                var sessionAttr = session.Attribute(key);
                if (sessionAttr != null && sessionAttr.Value.Contains(value, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        private Dictionary<string, string> GetAllWorkshopAttributes(XElement workshop)
        {
            var attributes = new Dictionary<string, string>();

            foreach (var attr in workshop.Attributes())
            {
                attributes[attr.Name.LocalName] = attr.Value;
            }

            foreach (var session in workshop.Elements("Session"))
            {
                foreach (var attr in session.Attributes())
                {
                    string sessionKey = $"Session-{attr.Name.LocalName}";
                    attributes[sessionKey] = attr.Value;
                }
            }

            return attributes;
        }
    }
}





