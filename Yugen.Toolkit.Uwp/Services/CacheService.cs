using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Threading.Tasks;

namespace Yugen.Toolkit.Uwp.Services
{
    public static class CacheService
    {
        private static readonly Dictionary<string, string> MemoryCache = new Dictionary<string, string>();

        private static ILogger _logger;

        public static void Init(ILogger logger = null)
        {
            _logger = logger;
        }

        public static async Task<T> GetItemAsync<T>(string fileName, string folderName = null)
        {
            string json;
            if (MemoryCache.ContainsKey(fileName))
            {
                json = MemoryCache[fileName];
            }
            else
            {
                var file = await UserStorageService.GetFile(fileName, folderName);
                json = await UserStorageService.ReadTextFromFileAsync(file);
                MemoryCache[fileName] = json;
            }

            if (string.IsNullOrEmpty(json)) return default;

            try
            {
                var data = JsonSerializer.Deserialize<T>(json);
                return data;
            }
            catch (Exception exception)
            {
                _logger?.LogDebug(exception, typeof(CacheService).ToString());
            }
            return default;
        }

        public static async Task<List<T>> GetItemsByPrefixAsync<T>(string prefix)
        {
            var results = new List<T>();

            var keys = MemoryCache.Keys.Where(k => k.StartsWith(prefix)).ToList();
            var inFileKeys = await UserStorageService.GetMatchingFilesByPrefixAsync(prefix, keys);

            keys.AddRange(inFileKeys);

            foreach (var key in keys)
            {
                var data = await GetItemAsync<T>(key);
                results.Add(data);
            }
            return results;
        }

        public static async Task ClearItemsByPrefixAsync(string prefix)
        {
            var keys = MemoryCache.Keys.Where(k => k.StartsWith(prefix)).ToList();

            foreach (var key in keys)
            {
                MemoryCache.Remove(key);
            }

            var inFileKeys = await UserStorageService.GetMatchingFilesByPrefixAsync(prefix, keys);

            foreach (var fileKey in inFileKeys)
            {
                await UserStorageService.DeleteFileIfExistsAsync(fileKey);
            }
        }

        public static async Task AddItemAsync<T>(string fileName, T data, string folderName = null)
        {
            await AddItemAsync(fileName, data, true, folderName);
        }

        public static async Task AddItemAsync<T>(string fileName, T data, bool useStorageCache, string folderName = null)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);

                if (useStorageCache)
                {
                    var file = await UserStorageService.GetFile(fileName, folderName);
                    await UserStorageService.WriteTextAsync(file, json);
                }

                if (MemoryCache.ContainsKey(fileName))
                {
                    MemoryCache[fileName] = json;
                }
                else
                {
                    MemoryCache.Add(fileName, json);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogDebug(exception, typeof(CacheService).ToString());
            }
        }

        private static bool CanPerformLoad(bool needNetwork)
        {
            return !needNetwork || NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
