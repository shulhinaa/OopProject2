using System.Diagnostics;
using lab2.XmlProcessingStrategy;

namespace lab2
{
    public class HtmlTransformer
    {
        public string TransformToHtml(string xmlContent)
        {
            if (string.IsNullOrWhiteSpace(xmlContent))
                throw new ArgumentException("XML content cannot be empty.", nameof(xmlContent));

            var outputFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Workshops.html");
            XmlToHtmlTransformer.TransformXmlToHtml(xmlContent, outputFilePath);

            OpenInBrowser(outputFilePath);
            return outputFilePath;
        }

        private void OpenInBrowser(string filePath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"file:///{filePath.Replace("\\", "/")}",
                UseShellExecute = true
            });
        }
    }
}
