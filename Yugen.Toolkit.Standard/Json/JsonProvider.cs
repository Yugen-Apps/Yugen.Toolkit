using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Yugen.Toolkit.Standard.Json
{
    public static class JsonProvider
    {
        /// <summary>
        /// Deserializes the JSON to a .NET object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The JSON to deserialize.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static async Task<T> ToObjectAsync<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            return await Task.Run(() => JsonConvert.DeserializeObject<T>(value));
        }

        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static async Task<string> StringifyAsync(object value) =>
            await Task.Run(() => JsonConvert.SerializeObject(value));

        /// <summary>
        /// Debug Deserializes the JSON to a .NET object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The JSON to deserialize.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static T DebugToObjectAsync<T>(string value)
        {
            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) =>
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }
                }
            };

            T result = JsonConvert.DeserializeObject<T>(value, settings);
            return result;
        }

        /// <summary>
        /// Clone an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}