using System;
using System.Text.Json;

namespace Yugen.Toolkit.Standard.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// Debug Deserializes the JSON to a .NET object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The JSON to deserialize.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static T DebugToObjectAsync<T>(string value)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Clone an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            var serialized = JsonSerializer.Serialize(source);
            return JsonSerializer.Deserialize<T>(serialized);
        }
    }
}