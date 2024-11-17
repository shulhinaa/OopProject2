using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace lab2.XmlProcessingStrategy
{
    public class XmlSAXProcessing : IXmlProcessing
    {
        public FoundResults Search(Dictionary<string, string> criteria, string content)
        {
            var results = new FoundResults();

            using (XmlReader reader = XmlReader.Create(new StringReader(content)))
            {
                string currentElement = "";
                Dictionary<string, string> attributes = new();
                bool isRelevantElement = false;

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            currentElement = reader.Name;

                            if (currentElement == "Workshop" || currentElement == "Session")
                            {
                                isRelevantElement = true;
                                

                                if (reader.HasAttributes)
                                {
                                    for (int i = 0; i < reader.AttributeCount; i++)
                                    {
                                        reader.MoveToAttribute(i); 
                                        attributes[reader.Name] = reader.Value;
                                    }

                                    reader.MoveToElement(); 
                                }
                            }
                            else
                            {
                                isRelevantElement = false;
                            }
                            break;

                        case XmlNodeType.EndElement:
                            if (isRelevantElement && (reader.Name == "Workshop" || reader.Name == "Session"))
                            {
                                if (MatchesCriteria(attributes, criteria))
                                {

                                    results.AddMatch(currentElement, attributes["id"], content);
                                }

                                isRelevantElement = false;
                            }
                            break;
                    }
                }
            }

            return results;
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
