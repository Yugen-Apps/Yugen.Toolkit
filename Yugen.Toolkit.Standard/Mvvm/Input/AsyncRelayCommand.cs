using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Yugen.Toolkit.Standard.Commands
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute = null;
        private readonly bool _canExecute;

        private bool _isRunning;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<Task> execute, bool canExecute = true)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute;

        public async void Execute(object parameter)
        {
            try
            {
                var task = _execute();
                if (task == null)
                {
                    return;
                }

                _isRunning = true;
                await task;
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}
