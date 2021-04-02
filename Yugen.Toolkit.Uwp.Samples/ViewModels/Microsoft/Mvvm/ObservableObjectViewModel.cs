using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Navigation;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Microsoft.Mvvm
{
    public class ObservableObjectViewModel : ViewModelBase
    {
        private PersonObservableObject _person = new PersonObservableObject();

        public ObservableObjectViewModel()
        {
            UpdateCommand = new RelayCommand(UpdateCommandBehavior);
        }

        public PersonObservableObject Person
        {
            get => _person;
            set => SetProperty(ref _person, value);
        }

        public IRelayCommand UpdateCommand { get; }

        public override void OnNavigatedTo(object parameter, IDictionary<string, object> state)
        {
            Person.Name = "My Name";
        }

        public void UpdateCommandBehavior() =>
            Person.Name = "My new name";
    }
}