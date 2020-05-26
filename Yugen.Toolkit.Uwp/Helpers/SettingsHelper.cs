using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Yugen.Toolkit.Standard.Providers;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class SettingsHelper
    {
        private static readonly ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

        public static async Task WriteAsync<T>(string key, T value)
        {
            var valueString = await JsonProvider.StringifyAsync(value);
            LocalSettings.Values[key] = valueString;
        }

        public static void Write<T>(string key, T value)
        {
            var valueString = JsonConvert.SerializeObject(value);
            LocalSettings.Values[key] = valueString;
        }

        public static async Task<T> ReadAsync<T>(string key) => LocalSettings.Values.TryGetValue(key, out var value)
                ? await JsonProvider.ToObjectAsync<T>((string)value)
                : default;

        public static T Read<T>(string key) => LocalSettings.Values.TryGetValue(key, out var value)
                ? JsonConvert.DeserializeObject<T>((string)value)
                : default;
    }
}