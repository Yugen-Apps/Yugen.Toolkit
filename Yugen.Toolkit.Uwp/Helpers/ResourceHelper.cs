using Windows.ApplicationModel.Resources;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class ResourceHelper
    {
        private static readonly ResourceLoader _resourceLoader = _resourceLoader ?? new ResourceLoader();

        public static string GetText(string key) =>
            _resourceLoader.GetString(key);
    }
}