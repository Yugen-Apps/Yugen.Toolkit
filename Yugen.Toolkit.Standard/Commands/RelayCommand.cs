using System;
using System.Windows.Input;

namespace Yugen.Toolkit.Standard.Commands
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute = null;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute?.Invoke((T)parameter);
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute = null;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute?.Invoke();
    }
}
