using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class ContentDialogHelper
    {
        /// <summary>
        /// Initializes a new instance of the ContentDialog class.
        /// Begins an asynchronous operation to show the dialog.
        /// </summary>
        /// <param name="content">A value that specifies the content of the dialog.</param>
        /// <param name="title">A value that specifies the title of the dialog.</param>
        /// <param name="closeButtonText">A value that specifies the text to display on the close button.</param>
        /// <returns></returns>
        public static async Task<ContentDialogResult> Alert(string content, string title, string closeButtonText) =>
            await ShowAsync(content, title, closeButtonText, null, null, null, null, ContentDialogButton.Close);

        /// <summary>
        /// Initializes a new instance of the ContentDialog class.
        /// Begins an asynchronous operation to show the dialog.
        /// </summary>
        /// <param name="content">A value that specifies the content of the dialog.</param>
        /// <param name="title">A value that specifies the title of the dialog.</param>
        /// <param name="closeButtonText">A value that specifies the text to display on the close button.</param>
        /// <param name="primaryCommand">A value that specifies  the command to invoke when the primary button is tapped.</param>
        /// <param name="primaryButtonText">A value that specifies the text to display on the primary button.</param>
        /// <param name="defaultButton">A value that indicates which button on the dialog is the default</param>
        /// <returns></returns>
        public static async Task<ContentDialogResult> Confirm(string content, string title, string closeButtonText,
            ICommand primaryCommand, string primaryButtonText, ContentDialogButton defaultButton = ContentDialogButton.Close) =>
                await ShowAsync(content, title, closeButtonText, primaryCommand, primaryButtonText, null, null, defaultButton);

        /// <summary>
        /// Initializes a new instance of the ContentDialog class.
        /// Begins an asynchronous operation to show the dialog.
        /// </summary>
        /// <param name="content">A value that specifies the content of the dialog.</param>
        /// <param name="title">A value that specifies the title of the dialog.</param>
        /// <param name="closeButtonText">A value that specifies the text to display on the close button.</param>
        /// <param name="primaryCommand">A value that specifies  the command to invoke when the primary button is tapped.</param>
        /// <param name="primaryButtonText">A value that specifies the text to display on the primary button.</param>
        /// <param name="secondaryCommand">A value that specifies  the command to invoke when the secondary button is tapped.</param>
        /// <param name="secondaryButtonText">A value that specifies the text to display on the secondary button.</param>
        /// <param name="defaultButton">A value that indicates which button on the dialog is the default</param>
        /// <returns></returns>
        public static async Task<ContentDialogResult> ConfirmDelete(string content, string title, string closeButtonText,
            ICommand primaryCommand, string primaryButtonText, ICommand secondaryCommand, string secondaryButtonText, ContentDialogButton defaultButton = ContentDialogButton.Close) =>
                await ShowAsync(content, title, closeButtonText, primaryCommand, primaryButtonText, secondaryCommand, secondaryButtonText, defaultButton);

        private static async Task<ContentDialogResult> ShowAsync(string content, string title, string closeButtonText, 
            ICommand primaryCommand, string primaryButtonText, ICommand secondaryCommand, string secondaryButtonText, ContentDialogButton defaultButton)
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = closeButtonText,
                DefaultButton = defaultButton
            };

            if (primaryCommand != null)
            {
                deleteFileDialog.PrimaryButtonText = primaryButtonText;
                deleteFileDialog.PrimaryButtonCommand = primaryCommand;
            }

            if (secondaryCommand != null)
            {
                deleteFileDialog.SecondaryButtonText = secondaryButtonText;
                deleteFileDialog.SecondaryButtonCommand = secondaryCommand;
            }

            return await deleteFileDialog.ShowAsync();
        }
    }
}