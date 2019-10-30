using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Yugen.Toolkit.Standard.Providers
{
    public static class JsonProvider
    {
        public static async Task<T> ToObjectAsync<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default(T);

            return await Task.Run(() => JsonConvert.DeserializeObject<T>(value));
        }
        
        public static async Task<string> StringifyAsync(object value) => 
            await Task.Run(() => JsonConvert.SerializeObject(value));

        public static T DebugToObjectAsync<T>(string value)
        {
            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) =>
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                        System.Diagnostics.Debugger.Break();
                }
            };

            var result = JsonConvert.DeserializeObject<T>(value, settings);
            return result;
        }

        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}