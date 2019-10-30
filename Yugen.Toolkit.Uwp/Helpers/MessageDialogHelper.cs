using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class MessageDialogHelper
    {
        public static async Task<IUICommand> Confirm(string message, string title, UICommand proceed, UICommand cancel, uint defaultCommandIndex = 1)
        {
            var messageDialog = new MessageDialog(message, title);

            // Add commands and set their callbacks
            messageDialog.Commands.Add(proceed);
            messageDialog.Commands.Add(cancel);
            messageDialog.DefaultCommandIndex = defaultCommandIndex;

            return await messageDialog.ShowAsync();
        }

        public static async Task<IUICommand> Alert(string message, string title = "Error")
        {
            var messageDialog = new MessageDialog(message, title);
            return await messageDialog.ShowAsync();
        }
    }
}