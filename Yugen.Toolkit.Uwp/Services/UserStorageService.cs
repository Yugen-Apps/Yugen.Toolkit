using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Provider;
using Windows.Storage.Search;
using Windows.Storage.Streams;
using Yugen.Toolkit.Standard.Core.Helpers;
using Yugen.Toolkit.Uwp.Helpers;

namespace Yugen.Toolkit.Uwp.Services
{
    public static class UserStorageService
    {
        public static async void CopyFile(string sourceFolder, string sourceFile, string targetFolderName = "", CreationCollisionOption creationCollisionOption = CreationCollisionOption.OpenIfExists)
        {

            StorageFolder folder = ApplicationData.Current.LocalFolder;

            if (!string.IsNullOrEmpty(targetFolderName))
                folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(targetFolderName, creationCollisionOption);

            var oldFile = await folder.TryGetItemAsync(sourceFile);
            if (oldFile != null) return;
            
            StorageFile newFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"{sourceFolder}{sourceFile}"));
            await newFile.CopyAsync(folder);
        }

        public static async Task<StorageFile> GetFile(string fileName, string folderName = "", CreationCollisionOption creationCollisionOption = CreationCollisionOption.OpenIfExists)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;

                if (!string.IsNullOrEmpty(folderName))
                    folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(folderName, creationCollisionOption);

                return await folder.CreateFileAsync(fileName, creationCollisionOption);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(UserStorageService), exception);
                return null;
            }
        }

        public static async Task<string> ReadTextFromFileAsync(StorageFile file)
        {
            try
            {
                if (file != null)
                {
                    string content = await FileIO.ReadTextAsync(file);
                    return content;
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(UserStorageService), exception);
            }
            return string.Empty;
        }

        public static async Task<IBuffer> ReadBufferFromFileAsync(StorageFile file)
        {
            try
            {
                if (file != null)
                {
                    var content = await FileIO.ReadBufferAsync(file);
                    return content;
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(UserStorageService), exception);
            }
            return null;
        }

        public static async Task<byte[]> ReadBytesFromFileAsync(StorageFile file)
        {
            try
            {
                if (file != null)
                {
                    byte[] result;
                    using (Stream stream = await file.OpenStreamForReadAsync())
                    {
                        using (var memoryStream = new MemoryStream())
                        {

                            stream.CopyTo(memoryStream);
                            result = memoryStream.ToArray();
                        }
                    }
                    return result;
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(UserStorageService), exception);
            }
            return null;
        }

        public static async Task WriteTextAsync(StorageFile file, string content)
        {
            try
            {
                await FileIO.WriteTextAsync(file, content);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(UserStorageService), exception);
            }
        }

        public static async Task WriteBufferAsync(StorageFile file, IBuffer fileContent)
        {
            try
            {
                if (file != null)
                {
                    // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                    CachedFileManager.DeferUpdates(file);

                    // write to file
                    await FileIO.WriteBufferAsync(file, fileContent);

                    // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                    // Completing updates may require Windows to ask for user input.
                    FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);

                    LoggerHelper.WriteLine(typeof(UserStorageService), status == FileUpdateStatus.Complete ? 
                                                                       $"File {file.Name} was saved." : 
                                                                       $"File {file.Name} couldn\'t be saved.");
                }
                else
                {
                    LoggerHelper.WriteLine(typeof(UserStorageService), "Operation cancelled.");
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(UserStorageService), exception);
            }
        }

        public static async Task WriteBytesAsync(StorageFile file, byte[] fileContent)
        {
            try
            {
                if (file != null)
                {
                    // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                    CachedFileManager.DeferUpdates(file);
                    // write to file
                    await FileIO.WriteBytesAsync(file, fileContent);

                    // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                    // Completing updates may require Windows to ask for user input.
                    FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);

                    LoggerHelper.WriteLine(typeof(UserStorageService), status == FileUpdateStatus.Complete ?
                                                                       $"File {file.Name} was saved." :
                                                                       $"File {file.Name} couldn\'t be saved.");
                }
                else
                {
                    LoggerHelper.WriteLine(typeof(UserStorageService), "Operation cancelled.");
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(UserStorageService), exception);
            }
        }

        public static async Task<List<string>> GetMatchingFilesByPrefixAsync(string prefix, List<string> excludeFiles)
        {
            try
            {
                var result = new List<string>();
                var folder = ApplicationData.Current.LocalFolder;
                var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new List<string>() { "*" })
                {
                    UserSearchFilter = $"{prefix}*.*",
                    FolderDepth = FolderDepth.Shallow,
                    IndexerOption = IndexerOption.UseIndexerWhenAvailable
                };
                var queryResult = folder.CreateFileQueryWithOptions(queryOptions);

                var matchingFiles = await queryResult.GetFilesAsync();

                if (matchingFiles.Count > 0)
                {
                    result.AddRange(
                        matchingFiles.Where(f => !excludeFiles.Contains(f.Name)).Select(f => f.Name)
                    );
                }
                return result;
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(UserStorageService), exception);
            }
            return null;
        }

        public static async Task DeleteFileIfExistsAsync(string fileName)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.GetFileAsync(fileName);
                if (file != null)
                {
                    await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(UserStorageService), exception);
            }
        }

        public static async Task AppendLineToFileAsync(string fileName, string line)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                await FileIO.AppendLinesAsync(file, new List<string>() { line });
            }
            catch (Exception exception)
            {
                /* Avoid any exception at this point. */
                LoggerHelper.WriteLine(typeof(UserStorageService), exception);
            }
        }

        public static async Task LoadFile(string oldfile, string fileExtension)
        {
            var file = await FilePickerHelper.OpenFile(fileExtension);
            if (file == null) return;

            var fileContent = await ReadBufferFromFileAsync(file);
            var folder = ApplicationData.Current.LocalFolder;
            var oldFile = await folder.CreateFileAsync(oldfile, CreationCollisionOption.OpenIfExists);

            await WriteBufferAsync(oldFile, fileContent);
        }

        public static async Task SaveFile(string oldfile, string fileName, string fileTypeName, string fileTypeExtension )
        {
            var dbFile = await GetFile(oldfile);
            var fileContent = await ReadBufferFromFileAsync(dbFile);
            var file = await FilePickerHelper.SaveFile(fileName, fileTypeName, fileTypeExtension);
            await WriteBufferAsync(file, fileContent);
        }
    }
}
