using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace lab2.XmlProcessingStrategy
{
    public static class XmlFileHandler
    {
        public static string ReadFileAsString(string filePath)
        {
            ValidateFilePath(filePath);
            return File.ReadAllText(filePath);
        }

        private static void ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file at '{filePath}' does not exist or the path is invalid.");
            }

            try
            {
                using (var reader = XmlReader.Create(filePath)) { }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"The file at '{filePath}' is not a valid XML file. Error: {ex.Message}");
            }
        }
    }
}
