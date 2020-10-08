
using System.Threading.Tasks;
using Windows.Storage;
using System.Text.Json;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class SettingsHelper
    {
        private static readonly ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

        //public static async Task WriteAsync<T>(string key, T value)
        //{
        //    var valueString = Task.Run(()=>JsonSerializer.Serialize(value));
        //    LocalSettings.Values[key] = valueString;
        //}

        public static void Write<T>(string key, T value)
        {
            var valueString = JsonSerializer.Serialize(value);
            LocalSettings.Values[key] = valueString;
        }

        //public static async Task<T> ReadAsync<T>(string key) => LocalSettings.Values.TryGetValue(key, out var value)
        //        ? await JsonProvider.ToObjectAsync<T>((string)value)
        //        : default;

        public static T Read<T>(string key) => LocalSettings.Values.TryGetValue(key, out var value)
                ? JsonSerializer.Deserialize<T>((string)value)
                : default;
    }
}