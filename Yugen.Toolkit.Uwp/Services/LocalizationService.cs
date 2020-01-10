using Windows.ApplicationModel.Resources;

namespace Yugen.Toolkit.Uwp.Services
{
    public static class LocalizationService
    {
        private static readonly ResourceLoader Loader = new ResourceLoader();

        public static string Load(string stringName)
        {
            var localizedString = Loader.GetString(stringName);
            return string.IsNullOrEmpty(localizedString) ? $"__{stringName}" : localizedString;
        }
    }
}