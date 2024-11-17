using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace lab2.XmlProcessingStrategy
{
    public class XmlDOMProcessing : IXmlProcessing
    {
        public FoundResults Search(Dictionary<string, string> criteria, string xmlContent)
        {
            var results = new FoundResults();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);


            var workshopNodes = xmlDoc.SelectNodes("//Workshop");

            if (workshopNodes != null)
            {
                foreach (XmlNode workshop in workshopNodes)
                {
                    var collectedAttributes = CollectAttributes(workshop);


                    if (MatchesCriteria(collectedAttributes, criteria))
                    {
                        results.AddMatch(workshop.Name, collectedAttributes["id"], xmlContent);
                    }
                }
            }

            return results;
        }

        private Dictionary<string, string> CollectAttributes(XmlNode workshop)
        {
            var attributes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (workshop.Attributes != null)
            {
                foreach (XmlAttribute attribute in workshop.Attributes)
                {
                    attributes[attribute.Name] = attribute.Value;
                }
            }


            foreach (XmlNode child in workshop.ChildNodes)
            {
                if (child.Attributes != null)
                {
                    foreach (XmlAttribute attribute in child.Attributes)
                    {
                        attributes[attribute.Name] = attribute.Value;
                    }
                }
            }

            return attributes;
        }

        private bool MatchesCriteria(Dictionary<string, string> attributes, Dictionary<string, string> criteria)
        {
            foreach (var criterion in criteria)
            {
                if (!attributes.ContainsKey(criterion.Key) ||
                    !attributes[criterion.Key].Contains(criterion.Value, StringComparison.OrdinalIgnoreCase))
                {
                    return false; 
                }
            }

            return true; 
        }
    }


}
