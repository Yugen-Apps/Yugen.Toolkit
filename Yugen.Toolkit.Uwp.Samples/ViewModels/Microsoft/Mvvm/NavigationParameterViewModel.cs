using System.Collections.Generic;
using Yugen.Toolkit.Standard.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class NavigationParameterViewModel : ViewModelBase
    {
        private string _parameter;

        private string _text;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public override void OnNavigatedTo(object parameter, IDictionary<string, object> state)
        {
            _parameter = parameter as string ?? string.Empty;

            Text = _parameter;
        }

        public void Button_Click(object _1, Windows.UI.Xaml.RoutedEventArgs _2)
        {
            Text = "I'm the new text";
        }
    }
}