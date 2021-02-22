using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Standard.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class CommandViewModel : ViewModelBase
    {
        private string _text;

        public CommandViewModel()
        {
            ShowTextCommand = new RelayCommand(ShowTextCommandBehavior);
            ShowDialogAsyncCommand = new AsyncRelayCommand(ShowDialogAsyncCommandBehavior);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ICommand ShowTextCommand { get; }

        public ICommand ShowDialogAsyncCommand { get; }

        private void ShowTextCommandBehavior() => Text = "Ciao";

        private async Task ShowDialogAsyncCommandBehavior() =>
            await new ContentDialog { Title = "Ciao", CloseButtonText = "Close" }.ShowAsync();
    }
}