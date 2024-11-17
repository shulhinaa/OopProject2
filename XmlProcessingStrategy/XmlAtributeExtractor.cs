using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace lab2.XmlProcessingStrategy
{
    public static class XmlAttributeExtractor
    {
        public static List<string> GetAttributesById(string xmlContent, string id)
        {
            var result = new List<string>();

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent);

                var nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
              
                var xpathQuery = $"//*[@id='{id}']";
                var node = xmlDoc.SelectSingleNode(xpathQuery, nsManager);

                if (node != null)
                {
                    if (node.Attributes != null)
                    {
                        foreach (XmlAttribute attr in node.Attributes)
                        {
                            result.Add(attr.Value);
                        }
                    }


                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Attributes != null)
                        {
                            foreach (XmlAttribute childAttr in childNode.Attributes)
                            {
                                result.Add(childAttr.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting attributes: {ex.Message}");
            }

            return result;
        }
    }
}
