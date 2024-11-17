using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.XmlProcessingStrategy
{
    public interface IXmlProcessing
    {
        FoundResults Search(Dictionary<string, string> criteria, string xmlContent);
    }
}
