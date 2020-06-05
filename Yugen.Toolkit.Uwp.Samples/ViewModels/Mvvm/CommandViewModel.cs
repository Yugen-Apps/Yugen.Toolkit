using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Standard.Mvvm.ComponentModel;
using Yugen.Toolkit.Standard.Mvvm.Input;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class CommandViewModel : ViewModelBase
    {
        private ICommand _buttonCommand;
        private ICommand _buttonAsyncCommand;
        private string _text;

        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        public ICommand ButtonCommand => _buttonCommand ?? (_buttonCommand = new RelayCommand(ButtonCommandBehavior));

        public ICommand ButtonAsyncCommand => _buttonAsyncCommand ?? (_buttonAsyncCommand = new AsyncRelayCommand(ButtonAyncCommandBehavior));

        private void ButtonCommandBehavior() => Text = "Ciao";

        private async Task ButtonAyncCommandBehavior() => await new ContentDialog { Title = "Ciao", CloseButtonText = "Close" }.ShowAsync();
    }
}