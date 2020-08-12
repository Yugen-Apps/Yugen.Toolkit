//using System;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using Yugen.Toolkit.Standard.Extensions;

//namespace Yugen.Toolkit.Standard.Mvvm.Input
//{
//    /// <summary>
//    /// A generic command that provides a more specific version of <see cref="AsyncRelayCommand"/>.
//    /// </summary>
//    /// <typeparam name="T">The type of parameter being passed as input to the callbacks.</typeparam>
//    public class AsyncRelayCommand<T> : ICommand
//    {
//        /// <summary>
//        /// The <see cref="Func{TResult}"/> to invoke when <see cref="Execute(T)"/> is used.
//        /// </summary>
//        private readonly Func<T, Task> _execute = null;

//        /// <summary>
//        /// The optional action to invoke when <see cref="CanExecute(T)"/> is used.
//        /// </summary>
//        private readonly Func<T, bool> _canExecute;

//        private bool _isExecuting;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="AsyncRelayCommand{T}"/> class that can always execute.
//        /// </summary>
//        /// <param name="execute">The execution logic.</param>
//        /// <param name="canExecute">The execution status logic.</param>
//        /// <remarks>See notes in <see cref="RelayCommand{T}(Action{T})"/>.</remarks>
//        public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute = null)
//        {
//            _execute = execute;
//            _canExecute = canExecute;
//        }

//        public event EventHandler CanExecuteChanged;

//        public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

//        public bool CanExecute(object parameter) => !_isExecuting && (_canExecute?.Invoke((T)parameter) != false);

//        public void Execute(object parameter)
//        {
//            ExecuteAsync(parameter).FireAndForgetSafeAsync();
//        }

//        public async Task ExecuteAsync(object parameter)
//        {
//            if (CanExecute(parameter))
//            {
//                try
//                {
//                    _isExecuting = true;
//                    await _execute((T)parameter);
//                }
//                finally
//                {
//                    _isExecuting = false;
//                }
//            }

//            NotifyCanExecuteChanged();
//        }
//    }
//}