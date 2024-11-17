using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace lab2.XmlProcessingStrategy
{
    public static class XmlToHtmlTransformer
    {
        public static void TransformXmlToHtml(string xmlContent, string outputFilePath)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string xsltFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Html/TableTemplate.xslt");

            if (string.IsNullOrWhiteSpace(xmlContent))
                throw new ArgumentException("XML content cannot be empty.", nameof(xmlContent));

            if (!File.Exists(xsltFullPath))
                throw new FileNotFoundException("XSLT file not found.", xsltFullPath);

            try
            {
                var xmlReader = XmlReader.Create(new StringReader(xmlContent));

                var xslt = new XslCompiledTransform();
                xslt.Load(xsltFullPath);

                using (var writer = XmlWriter.Create(outputFilePath, new XmlWriterSettings { Indent = true }))
                {
                    xslt.Transform(xmlReader, writer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error transforming XML to HTML: {ex.Message}");
                throw;
            }
        }
    }
}
