using System.Collections.Generic;
using System.Linq;

namespace Yugen.Toolkit.Standard.Extensions
{
    public static class DictionaryExtensions
    {
        private static string ToQueryString(this Dictionary<string, string> pairs)
        {
            var list = pairs.Select(pair => $"{pair.Key}={pair.Value}").ToList();
            return "?" + string.Join("&", list);
        }
    }
}