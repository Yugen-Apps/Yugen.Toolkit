﻿using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public class ZipArchiveHelper
    {
        public static async Task<ZipArchive> GetZipArchive(StorageFile zipFile)
        {
            if (zipFile == null)
                return null;

            Stream zipMemoryStream = await zipFile.OpenStreamForReadAsync();
            // Create zip archive to access compressed files in memory stream 
            return new ZipArchive(zipMemoryStream, ZipArchiveMode.Read);
        }

        public static ZipArchiveEntry GetZipArchiveEntryByName(ZipArchive zipArchive, string name) => zipArchive.Entries.FirstOrDefault(x => x.Name.Equals(name));

        public static ZipArchiveEntry GetZipArchiveEntryByFullName(ZipArchive zipArchive, string fullName) => zipArchive.Entries.FirstOrDefault(x => x.FullName.Equals(fullName));

        public static async Task UnzipFile(IStorageFile sourceZipFile, IStorageItem destinationFolder)
        {
            var zipMemoryStream = await sourceZipFile.OpenStreamForReadAsync();

            using (ZipArchive zipArchive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Read))
            {
                zipArchive.ExtractToDirectory(destinationFolder.Path);
            }
        }


        /// <summary>
        /// Operation just a awaitable task
        /// </summary>
        /// <param name="zipFile"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="DeleteZipFileSource"></param>
        /// <returns></returns>
        public static async Task UnZipFileAsync(StorageFile zipFile, StorageFolder destinationFolder, bool DeleteZipFileSource = false)
        {
            try
            {
                await UnZipFileAsync(zipFile, destinationFolder);
                if (zipFile != null && DeleteZipFileSource == true)
                {
                    await zipFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
            }
            catch
            {
                //LoggerHelper.WriteLine(typeof(ZipArchiveHelper), $"Failed to read file ...{ex.Message}");
            }
        }

        //Just Async
        private async static Task UnZipFileAsync(StorageFile zipFile, StorageFolder destinationFolder)
        {
            await UnZipFile(zipFile, destinationFolder).AsAsyncAction();
        }

        #region private helper functions 
        private static async Task UnZipFile(StorageFile zipFile, StorageFolder destinationFolder)
        {
            var extension = zipFile.FileType;
            if (zipFile == null || destinationFolder == null ||
                !extension.Equals(".zip", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ArgumentException("Invalid argument..." + extension);
            }

            Stream zipMemoryStream = await zipFile.OpenStreamForReadAsync();
            // Create zip archive to access compressed files in memory stream 
            using (ZipArchive zipArchive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Read))
            {
                // Unzip compressed file iteratively. 
                foreach (ZipArchiveEntry entry in zipArchive.Entries)
                {
                    await UnzipZipArchiveEntryAsync(entry, entry.FullName, destinationFolder);
                }
            }
        }

        /// <summary> 
        /// It checks if the specified path contains directory. 
        /// </summary> 
        /// <param name="entryPath">The specified path</param> 
        /// <returns></returns> 
        private static bool IfPathContainDirectory(string entryPath)
        {
            if (string.IsNullOrEmpty(entryPath))
            {
                return false;
            }
            return entryPath.Contains("/");
        }

        /// <summary> 
        /// It checks if the specified folder exists. 
        /// </summary> 
        /// <param name="storageFolder">The container folder</param> 
        /// <param name="subFolderName">The sub folder name</param> 
        /// <returns></returns> 
        private static async Task<bool> IfFolderExistsAsync(StorageFolder storageFolder, string subFolderName)
        {
            try
            {
                await storageFolder.GetFolderAsync(subFolderName);
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        /// <summary> 
        /// Unzips ZipArchiveEntry asynchronously. 
        /// </summary> 
        /// <param name="entry">The entry which needs to be unzipped</param> 
        /// <param name="filePath">The entry's full name</param> 
        /// <param name="unzipFolder">The unzip folder</param> 
        /// <returns></returns> 
        private static async Task UnzipZipArchiveEntryAsync(ZipArchiveEntry entry, string filePath, StorageFolder unzipFolder)
        {
            if (IfPathContainDirectory(filePath))
            {
                // Create sub folder 
                string subFolderName = Path.GetDirectoryName(filePath);
                bool isSubFolderExist = await IfFolderExistsAsync(unzipFolder, subFolderName);
                StorageFolder subFolder;
                if (!isSubFolderExist)
                {
                    // Create the sub folder. 
                    subFolder =
                        await unzipFolder.CreateFolderAsync(subFolderName, CreationCollisionOption.ReplaceExisting);
                }
                else
                {
                    // Just get the folder. 
                    subFolder =
                        await unzipFolder.GetFolderAsync(subFolderName);
                }
                // All sub folders have been created. Just pass the file name to the Unzip function. 
                string newFilePath = Path.GetFileName(filePath);
                if (!string.IsNullOrEmpty(newFilePath))
                {
                    // Unzip file iteratively. 
                    await UnzipZipArchiveEntryAsync(entry, newFilePath, subFolder);
                }
            }
            else
            {
                // Read uncompressed contents 
                using (Stream entryStream = entry.Open())
                {
                    byte[] buffer = new byte[entry.Length];
                    entryStream.Read(buffer, 0, buffer.Length);
                    // Create a file to store the contents 
                    StorageFile uncompressedFile = await unzipFolder.CreateFileAsync
                    (entry.Name, CreationCollisionOption.ReplaceExisting);
                    // Store the contents 
                    using (IRandomAccessStream uncompressedFileStream =
                    await uncompressedFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        using (Stream outstream = uncompressedFileStream.AsStreamForWrite())
                        {
                            outstream.Write(buffer, 0, buffer.Length);
                            outstream.Flush();
                        }
                    }
                }
            }
        }
        #endregion
    }
}