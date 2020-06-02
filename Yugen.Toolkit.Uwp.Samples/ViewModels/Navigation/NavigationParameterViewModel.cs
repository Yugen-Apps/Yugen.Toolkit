using System.Collections.Generic;
using Yugen.Toolkit.Standard.Mvvm.ComponentModel;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Navigation
{
    public class NavigationParameterViewModel : ViewModelBase
    {
        //private string _parameter;

        private string _text;
        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }


        private PersonObservableObject _category = new PersonObservableObject();
        public PersonObservableObject Category
        {
            get { return _category; }
            set { Set(ref _category, value); }
        }

        public override void OnNavigatedTo(object parameter, IDictionary<string, object> state)
        {
            Text = parameter as string ?? string.Empty;

            Category.Name = Text;
        }

        public void Page_Loaded(object _1, Windows.UI.Xaml.RoutedEventArgs _2)
        {
            //Text = _parameter;
        }

        public void Button_Tapped(object _1, Windows.UI.Xaml.Input.TappedRoutedEventArgs _2)
        {
            Text = "aaa";
        }
    }
}
