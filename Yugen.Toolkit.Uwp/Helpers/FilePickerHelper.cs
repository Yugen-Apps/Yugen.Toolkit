using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Yugen.Toolkit.Uwp.Helpers
{
    /// <summary>
    /// Helper to open and save a file using the picker
    /// </summary>
    public static class FilePickerHelper
    {
        /// <summary>
        /// Open a file from the picker
        /// </summary>
        /// <param name="fileTypeExtensionFilter">Extension eg: .jpg</param>
        /// <returns>StorageFile</returns>
        public static async Task<StorageFile> OpenFile(string fileTypeExtensionFilter) => await OpenFile(new List<string> { fileTypeExtensionFilter });

        /// <summary>
        /// Open a file from the picker
        /// </summary>
        /// <param name="fileTypeExtensionFilters">Extension List eg: .jpg, .png</param>
        /// <returns>StorageFile</returns>
        public static async Task<StorageFile> OpenFile(IList<string> fileTypeExtensionFilters = null)
        {
            FileOpenPicker picker = GeneratePicker(fileTypeExtensionFilters);

            return await picker.PickSingleFileAsync();
        }

        /// <summary>
        /// Open files from the picker
        /// </summary>
        /// <param name="fileTypeExtensionFilter">Extension eg: .jpg</param>
        /// <returns>StorageFile</returns>
        public static async Task<IReadOnlyList<StorageFile>> OpenFiles(string fileTypeExtensionFilter) => await OpenFiles(new List<string> { fileTypeExtensionFilter });

        /// <summary>
        /// Open files from the picker
        /// </summary>
        /// <param name="fileTypeExtensionFilters">Extension List eg: .jpg, .png</param>
        /// <returns>StorageFile</returns>
        public static async Task<IReadOnlyList<StorageFile>> OpenFiles(IList<string> fileTypeExtensionFilters = null)
        {
            FileOpenPicker picker = GeneratePicker(fileTypeExtensionFilters);

            return await picker.PickMultipleFilesAsync();
        }

        private static FileOpenPicker GeneratePicker(IList<string> fileTypeExtensionFilters)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            if (fileTypeExtensionFilters == null)
            {
                picker.FileTypeFilter.Add("*");
            }
            else
            {
                foreach (var extension in fileTypeExtensionFilters)
                {
                    picker.FileTypeFilter.Add(extension);
                }
            }

            return picker;
        }


        /// <summary>
        /// Save a file with the picker
        /// </summary>
        /// <param name="suggestedFileName">Suggested file name</param>
        /// <param name="fileTypeName">File type name eg: Image</param>
        /// <param name="fileTypeExtension">Extension eg: .jpg</param>
        /// <returns>StorageFile</returns>
        public static async Task<StorageFile> SaveFile(string suggestedFileName, string fileTypeName, string fileTypeExtension) =>
            await SaveFile(suggestedFileName, new Dictionary<string, List<string>>() { { fileTypeName, new List<string>() { fileTypeExtension } } });

        /// <summary>
        /// Save a file with the picker
        /// </summary>
        /// <param name="suggestedFileName">Suggested file name</param>
        /// <param name="fileTypeChoices">File type name List eg: Image(.jpg,.png), Text(.txt)</param>
        /// <returns>StorageFile</returns>
        public static async Task<StorageFile> SaveFile(string suggestedFileName, IDictionary<string, List<string>> fileTypeChoices)
        {
            FileSavePicker picker = new FileSavePicker
            {
                DefaultFileExtension = fileTypeChoices.First().Value.First(),
                SuggestedFileName = suggestedFileName,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            foreach (var filter in fileTypeChoices)
            {
                picker.FileTypeChoices.Add(filter.Key, filter.Value);
            }

            return await picker.PickSaveFileAsync();
        }
    }
}