using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Controls.Dialogs;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Controls
{
    public class YugenDialogViewModel : ViewModelBase
    {
        public YugenDialogViewModel()
        {
            ButtonCommand = new RelayCommand(ButtonCommandBehavior);
        }

        public ICommand ButtonCommand { get; }

        private async void ButtonCommandBehavior()
        {
            var yugenDialog = new YugenDialog();

            yugenDialog.Title = "aa";
            yugenDialog.CloseButtonText = "close";

            await yugenDialog.ShowAsync();
        }
    }
}