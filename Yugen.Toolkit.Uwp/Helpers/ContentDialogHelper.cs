using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class ContentDialogHelper
    {
        public static async Task<ContentDialogResult> Alert(string content, string title = "")
        {
            return await ConfirmDelete(content, title, null, null);
        }

        public static async Task<ContentDialogResult> Confirm(string content, string title, ICommand primaryCommand, ContentDialogButton defaultButton = ContentDialogButton.Close)
        {
            return await ConfirmDelete(content, title, primaryCommand, null, defaultButton);
        }

        public static async Task<ContentDialogResult> ConfirmDelete(string content, string title, ICommand primaryCommand, ICommand secondaryCommand, ContentDialogButton defaultButton = ContentDialogButton.Close)
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Close",
                DefaultButton = defaultButton
            };

            if (primaryCommand != null)
            {
                deleteFileDialog.PrimaryButtonText = "Ok";
                deleteFileDialog.PrimaryButtonCommand = primaryCommand;
            }

            if (secondaryCommand != null)
            {
                deleteFileDialog.SecondaryButtonText = "Delete";
                deleteFileDialog.SecondaryButtonCommand = secondaryCommand;
            }

            return await deleteFileDialog.ShowAsync();
        }
    }
}