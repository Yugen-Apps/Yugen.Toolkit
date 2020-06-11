using Yugen.Toolkit.Standard.Mvvm.ComponentModel;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Controls
{
    public class SampleInAppControlViewModel : ViewModelBase
    {
        private string _text = "aa";

        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        public SampleInAppControlViewModel() { }

        public SampleInAppControlViewModel(string text)
        {
            Text = text;
        }
    }
}
