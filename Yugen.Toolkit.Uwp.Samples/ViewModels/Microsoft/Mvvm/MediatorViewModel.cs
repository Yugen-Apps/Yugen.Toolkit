using Windows.UI.Xaml;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Standard.Mvvm.Mediator;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class MediatorViewModel : ViewModelBase
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Mediator.Instance.Register("one", (object o) => Text = o.ToString());
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Mediator.Instance.NotifyColleagues("one", "I'm a notification");
        }
    }
}