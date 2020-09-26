using Yugen.Toolkit.Standard.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Controls
{
    public class SampleInAppControlViewModel : ViewModelBase
    {
        private string _text = "aa";

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public SampleInAppControlViewModel() { }

        public SampleInAppControlViewModel(string text)
        {
            Text = text;
        }
    }
}
