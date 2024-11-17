using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab2.XmlProcessingStrategy
{
    public class FoundResults
    {
        public List<Match> Matches { get; }
        public string XmlContent { get; set; }

        public FoundResults()
        {
            Matches = new List<Match>();
        }

        public void AddMatch(string attributeName, string elementName, string xmlContent)
        {
            Matches.Add(new Match
            {
                ElementName = elementName,
                AttributeName = attributeName
            });
            XmlContent = xmlContent ?? string.Empty;
        }

        private List<string> CheckIds()
        {
            List<string> ids = new List<string>();
            foreach (Match match in Matches)
            {
                ids.Add(match.ElementName);
            }
            return ids;
        }

        public List<string> GetResultsAsStrings()
        {
            if (string.IsNullOrEmpty(XmlContent))
                return new List<string>();

            var ids = CheckIds();

            if (ids.Count == 1)
            {
                return XmlAttributeExtractor.GetAttributesById(XmlContent, ids[0]);
            }
            else if (ids.Count > 1)
            {
                string firstId = ids.First();
                bool allSame = ids.All(id => id == firstId);

                if (allSame)
                {
                    return XmlAttributeExtractor.GetAttributesById(XmlContent, firstId);
                }
                else
                {
                    var result = new List<string>();
                    foreach (var id in ids)
                    {
                        result.AddRange(XmlAttributeExtractor.GetAttributesById(XmlContent, id));
                    }
                    return result;
                }
            }

            
            return new List<string>();
        }
    }
}

