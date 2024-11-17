using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace lab2
{
    public class FileHandler
    {
        public string XmlContent { get; private set; }
        public string LoadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty.", nameof(filePath));

            if (!filePath.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Selected file is not an XML file.");

            XmlContent = File.ReadAllText(filePath);

            if (!IsValidXml(XmlContent))
                throw new InvalidOperationException("The selected file is not a valid XML document.");

            return XmlContent;
        }

        private bool IsValidXml(string content)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(content);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
