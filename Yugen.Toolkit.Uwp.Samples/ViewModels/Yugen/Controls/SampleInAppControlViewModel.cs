using Yugen.Toolkit.Standard.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Controls
{
    public class SampleInAppControlViewModel : ViewModelBase
    {
        private string _text = "Text";
        private string _text2 = "Text2";

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public string Text2
        {
            get => _text2;
            set => SetProperty(ref _text2, value);
        }
    }
}