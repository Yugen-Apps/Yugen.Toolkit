using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Common.Standard.Helpers;
using Common.Standard.Providers;

namespace Common.Uwp.Services
{
    public static class CacheService
    {
        private static readonly Dictionary<string, string> MemoryCache = new Dictionary<string, string>();

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

            if (string.IsNullOrEmpty(json)) return default(T);

            try
            {
                var data = await JsonProvider.ToObjectAsync<T>(json);
                return data;
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(CacheService), exception);
            }
            return default(T);
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
                var json = await JsonProvider.StringifyAsync(data);

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
                LoggerHelper.WriteLine(typeof(CacheService), exception);
            }
        }

        private static bool CanPerformLoad(bool needNetwork)
        {
            return !needNetwork || NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
