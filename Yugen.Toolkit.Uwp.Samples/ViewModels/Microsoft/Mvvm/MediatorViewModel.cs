using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Standard.Mvvm.Mediator;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm
{
    public class MediatorViewModel : ViewModelBase
    {
        private string _text;

        public MediatorViewModel()
        {
            LoadedCommand = new RelayCommand(LoadedCommandBehavior);
            NotifyCommand = new RelayCommand(NotifyCommandCommandBehavior);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ICommand LoadedCommand { get; }
        public ICommand NotifyCommand { get; }

        public void LoadedCommandBehavior() => 
            Mediator.Instance.Register("one", (object o) => Text = o.ToString());

        public void NotifyCommandCommandBehavior() => 
            Mediator.Instance.NotifyColleagues("one", "I'm a notification");
    }
}