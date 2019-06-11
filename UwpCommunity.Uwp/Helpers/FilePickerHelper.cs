using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace UwpCommunity.Uwp.Helpers
{
    public static class FilePickerHelper
    {
        public static async Task<StorageFile> OpenDbFile(string fileTypeFilter)
        {
            FileOpenPicker openPicker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                FileTypeFilter = { fileTypeFilter }
            };
            return await openPicker.PickSingleFileAsync();
        }

        public static async Task<StorageFile> SaveDbFile(string defaultFileExtension, string suggestedFileName)
        {
            FileSavePicker savePicker = new FileSavePicker
            {
                DefaultFileExtension = defaultFileExtension,
                SuggestedFileName = suggestedFileName,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            savePicker.FileTypeChoices.Add("Database", new List<string> { defaultFileExtension });
            return await savePicker.PickSaveFileAsync();
        }
    }
}