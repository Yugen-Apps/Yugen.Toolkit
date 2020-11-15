using System.Linq;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Yugen.Toolkit.Uwp.Constants;

namespace Yugen.Toolkit.Uwp.Samples.Helpers
{
    public static class DocHelper
    {
        public static string ReadSummary(string className)
        {
            var filePath = $"{Package.Current.InstalledLocation.Path}\\{UwpConstants.FolderAssets}\\Yugen.Toolkit.Uwp.Samples.XML";
            var xml = XElement.Load(filePath);
            return xml.Descendants("member")
                             .FirstOrDefault(m => m.Attribute("name")
                                .Value.Equals($"T:Yugen.Toolkit.Uwp.Samples.Views.{className}"))
                                    ?.Element("summary")?.Value;
        }
    }
}