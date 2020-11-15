using System.Collections.Generic;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Models;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Navigation;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class ObservableObjectViewModel : ViewModelBase
    {
        private PersonObservableObject _person = new PersonObservableObject();

        public PersonObservableObject Person
        {
            get => _person;
            set => SetProperty(ref _person, value);
        }

        public override void OnNavigatedTo(object parameter, IDictionary<string, object> state)
        {
            Person.Name = "My Name";

            Person p = Person;
        }

        public void Button_Click(object _1, Windows.UI.Xaml.RoutedEventArgs _2)
        {
            Person.Name = "My new name";
        }
    }
}