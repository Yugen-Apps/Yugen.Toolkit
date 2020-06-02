using Yugen.Toolkit.Standard.Mvvm.ComponentModel;
using Yugen.Toolkit.Uwp.Samples.Models;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Navigation
{
    public class PersonObservableObject : ObservableObject<Person>
    {
        public PersonObservableObject(Person model = null) : base(model) { }

        public string Name
        {
            get { return Model.Name; }
            set => Set(() => Model.Name, value);
            //set { Set(Model.Name, value, () => Model.Name = value); }
            //set { Set(Model, value); }
        }
    }
}
