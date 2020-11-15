using Yugen.Toolkit.Standard.Mvvm.ComponentModel;
using Yugen.Toolkit.Uwp.Samples.Models;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Navigation
{
    public class PersonObservableObject : ObservableObject<Person>
    {
        public PersonObservableObject(Person model = null) : base(model)
        {
        }

        public string Name
        {
            get => Model.Name;
            //set => SetProperty(Model.Name, value, Model, (m, v) => m.Name = v);
            set => SetProperty(Model.Name, value, (v) => Model.Name = value);
        }
    }
}