using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace WaveFormSample.Uwp
{
    public static class FilePickerHelper
    {
        public static async Task<StorageFile> OpenFile(IList<string> fileTypeExtensionFilters = null, PickerLocationId pickerLocationId = PickerLocationId.Downloads)
        {
            FileOpenPicker picker = GeneratePicker(fileTypeExtensionFilters, pickerLocationId);
            return await picker.PickSingleFileAsync();
        }

        private static FileOpenPicker GeneratePicker(IList<string> fileTypeExtensionFilters, PickerLocationId pickerLocationId)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                SuggestedStartLocation = pickerLocationId
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
    }
}