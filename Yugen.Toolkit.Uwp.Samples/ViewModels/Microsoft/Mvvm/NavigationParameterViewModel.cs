using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows.Input;
using Yugen.Toolkit.Standard.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class NavigationParameterViewModel : ViewModelBase
    {
        private string _parameter;
        private string _text;

        public NavigationParameterViewModel()
        {
            UpdateTextCommand = new RelayCommand(UpdateTextCommandBehavior);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ICommand UpdateTextCommand { get;  }

        public override void OnNavigatedTo(object parameter, IDictionary<string, object> state)
        {
            _parameter = parameter as string ?? string.Empty;

            Text = _parameter;
        }

        public void UpdateTextCommandBehavior() => 
            Text = "I'm the new text";
    }
}