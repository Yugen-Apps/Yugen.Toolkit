using System.Text.Json;
using Windows.Storage;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class SettingsHelper
    {
        private static readonly ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

        public static void Write<T>(string key, T value)
        {
            var valueString = JsonSerializer.Serialize(value);
            LocalSettings.Values[key] = valueString;
        }

        public static T Read<T>(string key) => LocalSettings.Values.TryGetValue(key, out var value)
                ? JsonSerializer.Deserialize<T>((string)value)
                : default;
    }
}