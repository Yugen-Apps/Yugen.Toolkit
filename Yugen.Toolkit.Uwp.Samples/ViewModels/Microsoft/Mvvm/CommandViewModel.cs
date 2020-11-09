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
            ButtonCommand = new RelayCommand(ButtonCommandBehavior);
            ButtonAsyncCommand = new AsyncRelayCommand(ButtonAyncCommandBehavior);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ICommand ButtonCommand { get; }

        public ICommand ButtonAsyncCommand { get; }

        private void ButtonCommandBehavior() => Text = "Ciao";

        private async Task ButtonAyncCommandBehavior() =>
            await new ContentDialog { Title = "Ciao", CloseButtonText = "Close" }.ShowAsync();
    }
}