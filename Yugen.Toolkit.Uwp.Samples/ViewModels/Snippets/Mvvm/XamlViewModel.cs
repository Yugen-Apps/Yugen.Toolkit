using Yugen.Toolkit.Standard.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class XamlViewModel : ViewModelBase
    {
        private string _text;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
    }
}