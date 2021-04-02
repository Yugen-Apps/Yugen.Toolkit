using Microsoft.Toolkit.Mvvm.Input;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Standard.Mvvm.Mediator;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Microsoft.Mvvm
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

        public IRelayCommand LoadedCommand { get; }
        public IRelayCommand NotifyCommand { get; }

        public void LoadedCommandBehavior() =>
            Mediator.Instance.Register("one", (o) => Text = o.ToString());

        public void NotifyCommandCommandBehavior() =>
            Mediator.Instance.NotifyColleagues("one", "I'm a notification");
    }
}