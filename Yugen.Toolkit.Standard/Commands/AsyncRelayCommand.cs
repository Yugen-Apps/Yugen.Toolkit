using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Yugen.Toolkit.Standard.Commands
{
    public class AsyncRelayCommand<T> : ICommand
    {
        private readonly Func<Task<T>> _execute = null;

        private bool _canExecute;
        private bool _isRunning;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<Task<T>> execute, bool canExecute = true)
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

    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute = null;

        private bool _canExecute;
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
